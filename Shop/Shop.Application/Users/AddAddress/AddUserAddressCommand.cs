using Common.Application;
using Common.Domain.ValueObjects;

namespace Shop.Application.Users.AddAddress;

/// <summary></summary>
/// <param name="UserId"></param>
/// <param name="Shire"></param>
/// <param name="City"></param>
/// <param name="PostalCode"></param>
/// <param name="PostalAddress"></param>
/// <param name="PhoneNumber"></param>
/// <param name="Name"></param>
/// <param name="Family"></param>
/// <param name="NationalCode"></param>
public record AddUserAddressCommand(
    long UserId,
    string Shire,
    string City,
    string PostalCode,
    string PostalAddress,
    PhoneNumber PhoneNumber,
    string Name,
    string Family,
    string NationalCode
)
    : IBaseCommand;