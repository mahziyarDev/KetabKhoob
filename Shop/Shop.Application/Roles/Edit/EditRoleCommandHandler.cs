using Common.Application;
using Shop.Domain.RoleAgg;
using Shop.Domain.RoleAgg.Repository;

namespace Shop.Application.Roles.Edit;

public class EditRoleCommandHandler : IBaseCommandHandler<EditRoleCommand>
{
    private readonly IRoleRepository _roleRepository;
    /// <summary></summary>
    /// <param name="roleRepository"></param>
    public EditRoleCommandHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }
    public async Task<OperationResult> Handle(EditRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetTrackingAsync(request.Id,cancellationToken);
        if (role == null) return OperationResult.NotFound();
        
        role.Edit(request.Title);
        var permissions = new List<RolePermission>();
        request.Permissions.ForEach(x =>
        {
            permissions.Add(new RolePermission(x));
        });
        role.SetPermissions(permissions);
        await _roleRepository.SaveChangeAsync(cancellationToken);
        return OperationResult.Success();
    }
}