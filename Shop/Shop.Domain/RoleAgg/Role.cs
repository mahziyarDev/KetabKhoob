using Common.Domain;

namespace Shop.Domain.RoleAgg
{
    public class Role : AggregateRoot
    {
        public Role(string title, List<RolePermission> permissions) 
        {
            Title = title;
            Permissions = permissions;
        }
        public Role(string title)
        {
            Title = title;
            Permissions = new List<RolePermission>();
        }

        public string Title { get; private set; }
        public List<RolePermission> Permissions { get; private set; }

    }
    public class RolePermission : BaseEntity
    {

    }

    public enum Permission 
    {
        PanelAdmin,
        EditProfile,
        ChangePassword,

    }
}
