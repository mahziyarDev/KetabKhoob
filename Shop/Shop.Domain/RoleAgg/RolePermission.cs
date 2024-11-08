using Common.Domain;
using Shop.Domain.RoleAgg.Enum;

namespace Shop.Domain.RoleAgg;

public class RolePermission : BaseEntity
{
    public long RoleId { get; set; }
    public Permission Permission { get; set; }
}