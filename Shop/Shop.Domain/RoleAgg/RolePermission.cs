using Common.Domain;
using Shop.Domain.RoleAgg.Enum;

namespace Shop.Domain.RoleAgg;

public class RolePermission(Permission permission) : BaseEntity
{
    public long RoleId { get; internal set; }
    public Permission Permission { get; private set; } = permission;
}