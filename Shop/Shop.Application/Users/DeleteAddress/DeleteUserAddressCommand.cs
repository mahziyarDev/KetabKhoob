using Common.Application;
using Shop.Domain.UserAgg.Repository;

namespace Shop.Application.Users.DeleteAddress;
/// <summary></summary>
/// <param name="UserId"></param>
/// <param name="AddressId"></param>
public record DeleteUserAddressCommand
(
    long UserId,
    long AddressId
)
: IBaseCommand;


public class DeleteUserAddressCommandHandler : IBaseCommandHandler<DeleteUserAddressCommand> {
    private readonly IUserRepository _userRepository;

    public DeleteUserAddressCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<OperationResult> Handle(DeleteUserAddressCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetTracking(request.UserId,cancellationToken);
        if (user == null)
            return OperationResult.NotFound();

        user.DeleteAddress(request.AddressId);

        await _userRepository.SaveChangeAsync(cancellationToken);
        return OperationResult.Success();
    }
}