using Common.Domain;
using Common.Domain.Exceptions;
using Shop.Domain.UserAgg.Enum;
using Shop.Domain.UserAgg.Services;

namespace Shop.Domain.UserAgg;

public class User : AggregateRoot
{
    public string Name { get; private set; }
    public string Family { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public Gender Gender { get; private set; }
    public List<UserRole> Roles { get; private set; }
    public List<Wallet> Wallets { get; private set; }
    public List<UserAddress> Addresses { get; private set; }

    public User(string name, string family, string phoneNumber, string email, string password,
        Gender gender,
        IDomainUserService domainUserService)
    {
        Guard(phoneNumber,email,domainUserService);
        Name = name;
        Family = family;
        PhoneNumber = phoneNumber;
        Email = email;
        Password = password;
        Gender = gender;
    }

    public static User RegisterUser(string email,string phoneNumber, string password,IDomainUserService domainUserService)
    {
        return new User("","",phoneNumber,email,password,Enum.Gender.None,domainUserService);
    }
    public void Edit(string name, string family, string phoneNumber, string email,
        Gender gender,
        IDomainUserService domainUserService)
    {
        Guard(phoneNumber,email,domainUserService);
        Name = name;
        Family = family;
        PhoneNumber = phoneNumber;
        Email = email;
        Gender = gender;
    }

    public void AddAddresses(UserAddress address)
    {
        address.UserId = Id;
        Addresses.Add(address);
    }

    public void EditAddresses(UserAddress address)
    {
        var oldAddress = Addresses.FirstOrDefault(x => x.Id == address.Id);
        if (oldAddress == null)
        {
            throw new NullOrEmptyDomainDataException("Address not found");
        }

        Addresses.Remove(oldAddress);
        Addresses.Add(address);
    }

    public void DeleteAddresses(UserAddress address)
    {
        var oldAddress = Addresses.FirstOrDefault(x => x.Id == address.Id);
        if (oldAddress == null)
        {
            throw new NullOrEmptyDomainDataException("Address not found");
        }

        Addresses.Remove(oldAddress);
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

    public void Guard(string phoneNumber, string email,IDomainUserService domainUserService)
    {
        NullOrEmptyDomainDataException.CheckString(phoneNumber,nameof(phoneNumber));
        NullOrEmptyDomainDataException.CheckString(email,nameof(email));

        if (phoneNumber.Length != 11)
            throw new InvalidDomainDataException("شماره موبایل نامعتبر است");
        
        if(PhoneNumber != phoneNumber)
            if (domainUserService.IsPhoneNumberExist(phoneNumber))
                throw new InvalidCastException("شماره تلفن وارده شده نامعتبر است");

        
        if (email.IsValidEmail() == false)
            throw new InvalidDomainDataException("ایمیل نامعتبر است");
        
        if(Email != email)
            if (domainUserService.IsEmailExist(email))
                throw new InvalidCastException("ایمیل وارده شده نامعتبر است");
        
       
    }
}