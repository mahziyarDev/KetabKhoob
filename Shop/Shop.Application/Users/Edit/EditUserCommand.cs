using Common.Application;
using Microsoft.AspNetCore.Http;
using Shop.Domain.UserAgg.Enum;

namespace Shop.Application.Users.Edit;

public record EditUserCommand(
    long UserId,
    IFormFile? Avatar,
    string Name,
    string Family,
    string PhoneNumber,
    string Email,
    Gender Gender
)
    : IBaseCommand;