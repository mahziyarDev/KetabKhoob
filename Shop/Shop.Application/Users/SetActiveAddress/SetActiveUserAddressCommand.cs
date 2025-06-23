using Common.Application;
using Shop.Domain.UserAgg.Repository;

namespace Shop.Application.Users.SetActiveAddress;

public record SetActiveUserAddressCommand(long UserId, long AddressId) : IBaseCommand;

public class SetActiveUserAddressCommandHandler : IBaseCommandHandler<SetActiveUserAddressCommand>
{
    private readonly IUserRepository _repository;

    public SetActiveUserAddressCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(SetActiveUserAddressCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetTrackingAsync(request.UserId,cancellationToken);
        if (user == null)
            return OperationResult.NotFound();

        user.SetActiveAddress(request.AddressId);
        await _repository.SaveChangeAsync(cancellationToken);
        return OperationResult.Success();
    }
}