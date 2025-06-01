using Common.Application;
using Common.Domain.ValueObjects;

namespace Shop.Application.Users.Register;

public record RegisterCommand 
    (
    PhoneNumber PhoneNumber ,
    string Password 
    )
    : IBaseCommand;
