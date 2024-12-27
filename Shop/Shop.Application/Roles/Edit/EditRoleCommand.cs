using Common.Application;
using Shop.Domain.RoleAgg.Enum;

namespace Shop.Application.Roles.Edit;

/// <summary></summary>
/// <param name="Id"></param>
/// <param name="Title"></param>
/// <param name="Permissions"></param>
public record EditRoleCommand
    (
        long Id,
        string Title,
        List<Permission> Permissions
    ) : IBaseCommand; 