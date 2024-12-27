using Common.Application;
using Shop.Domain.RoleAgg.Enum;

namespace Shop.Application.Roles.Create;

/// <summary></summary>
/// <param name="Title"></param>
/// <param name="Permissions"></param>
public record CreateRoleCommand
    (
        string Title,
        List<Permission> Permissions
    ) : IBaseCommand;