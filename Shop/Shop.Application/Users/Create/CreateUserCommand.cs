using Common.Application;
using Shop.Domain.UserAgg.Enum;

namespace Shop.Application.Users.Create;

/// <summary></summary>
/// <param name="Name"></param>
/// <param name="Family"></param>
/// <param name="PhoneNumber"></param>
/// <param name="Email"></param>
/// <param name="Password"></param>
/// <param name="Gender"></param>
public record CreateUserCommand(
    string Name,
    string Family,
    string PhoneNumber,
    string Email,
    string Password,
    Gender Gender
) : IBaseCommand;