using Common.Application;
using Shop.Domain.RoleAgg;
using Shop.Domain.RoleAgg.Repository;

namespace Shop.Application.Roles.Create;

public class CreateRoleCommandHandler : IBaseCommandHandler<CreateRoleCommand>
{
    private readonly IRoleRepository _roleRepository;
    /// <summary></summary>
    /// <param name="roleRepository"></param>
    public CreateRoleCommandHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<OperationResult> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var permissions = new List<RolePermission>();
        request.Permissions.ForEach(x =>
        {
            permissions.Add(new RolePermission(x));
        });
        var role = new Role(request.Title, permissions);
        await _roleRepository.AddAsync(role);
        await _roleRepository.SaveChangeAsync();
        return OperationResult.Success();
    }
}