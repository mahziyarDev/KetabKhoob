using Common.Domain;
using Common.Domain.Exceptions;
using Shop.Domain.UserAgg.Enum;
using Shop.Domain.UserAgg.Services;

namespace Shop.Domain.UserAgg;

public class User : AggregateRoot
{
    #region Properties

    public string Name { get; private set; }
    public string AvatarName { get; set; }
    public string Family { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Email { get; private set; }
    public bool IsActive { get; set; }
    public string Password { get; private set; }
    public Gender Gender { get; private set; }
    public List<UserRole> Roles { get; private set; }
    public List<Wallet> Wallets { get; private set; }
    public List<UserAddress> Addresses { get; private set; }
    public List<UserToken> Tokens { get; }

    #endregion
    private User() { }

    public User(string name, string family, string phoneNumber, string email, string password,
        Gender gender,
        IUserDomainService domainUserService)
    {
        Guard(phoneNumber, email, domainUserService);
        Name = name;
        Family = family;
        PhoneNumber = phoneNumber;
        Email = email;
        Password = password;
        Gender = gender;
        Tokens = new();
    }

    public static User RegisterUser(string phoneNumber, string password, IUserDomainService userDomainService)
    {
        return new User("", "", phoneNumber, null, password, Gender.None, userDomainService);
    }

    public void Edit(string name, string family, string phoneNumber, string email,
        Gender gender,
        IUserDomainService domainUserService)
    {
        Guard(phoneNumber, email, domainUserService);
        Name = name;
        Family = family;
        PhoneNumber = phoneNumber;
        Email = email;
        Gender = gender;
    }
    public void SetAvatar(string imageName)
    {
        if (string.IsNullOrWhiteSpace(imageName))
            imageName = "avatar.png";

        AvatarName = imageName;
    }
    public void AddAddress(UserAddress address)
    {
        address.UserId = Id;
        Addresses.Add(address);
    }
    public void ChangePassword(string newPassword)
    {
        NullOrEmptyDomainDataException.CheckString(newPassword, nameof(newPassword));

        Password = newPassword;
    }
    public void AddToken(string hashJwtToken, string hashRefreshToken, DateTime tokenExpireDate, DateTime refreshTokenExpireDate, string device)
    {
        var activeTokenCount = Tokens.Count(c => c.RefreshTokenExpireDate > DateTime.Now);
        if (activeTokenCount == 3)
            throw new InvalidDomainDataException("امکان استفاده از 4 دستگاه همزمان وجود ندارد");

        var token = new UserToken(hashJwtToken, hashRefreshToken, tokenExpireDate, refreshTokenExpireDate, device);
        token.UserId = Id;
        Tokens.Add(token);
    }

    public string RemoveToken(long tokenId)
    {
        var token = Tokens.FirstOrDefault(f => f.Id == tokenId);
        if (token == null)
            throw new InvalidDomainDataException("invalid TokenId");

        Tokens.Remove(token);
        return token.HashJwtToken;
    }
    public void EditAddress(UserAddress address, long addressId)
    {
        var oldAddress = Addresses.FirstOrDefault(f => f.Id == addressId);
        if (oldAddress == null)
            throw new NullOrEmptyDomainDataException("Address Not found");


        oldAddress.Edit(address.Shire, address.City, address.PostalCode, address.PostalAddress,
            address.Name, address.Family, address.NationalCode, address.PhoneNumber);
    }
    public void DeleteAddress(long addressId)
    {
        var oldAddress = Addresses.FirstOrDefault(f => f.Id == addressId);
        if (oldAddress == null)
            throw new NullOrEmptyDomainDataException("Address Not found");

        Addresses.Remove(oldAddress);
    }
    public void SetActiveAddress(long addressId)
    {
        var currentAddress = Addresses.FirstOrDefault(f => f.Id == addressId);
        if (currentAddress == null)
            throw new NullOrEmptyDomainDataException("Address Not found");

        foreach (var address in Addresses)
        {
            address.SetDeActive();
        }
        currentAddress.SetActive();
    }
    public void ChargeWallet(Wallet wallet)
    {
        Wallets.Add(wallet);
    }

    public void SetRoles(List<UserRole> userRoles)
    {
        userRoles.ForEach(x => x.UserId = Id);
        Roles.Clear();
        Roles.AddRange(Roles);
    }

    private void Guard(string phoneNumber, string email, IUserDomainService domainUserService)
    {
        NullOrEmptyDomainDataException.CheckString(phoneNumber, nameof(phoneNumber));
        NullOrEmptyDomainDataException.CheckString(email, nameof(email));

        if (phoneNumber.Length != 11)
            throw new InvalidDomainDataException("شماره موبایل نامعتبر است");

        if (PhoneNumber != phoneNumber)
            if (domainUserService.IsPhoneNumberExist(phoneNumber))
                throw new InvalidCastException("شماره تلفن وارده شده نامعتبر است");


        if (email.IsValidEmail() == false)
            throw new InvalidDomainDataException("ایمیل نامعتبر است");

        if (Email != email)
            if (domainUserService.IsEmailExist(email))
                throw new InvalidCastException("ایمیل وارده شده نامعتبر است");


    }
}