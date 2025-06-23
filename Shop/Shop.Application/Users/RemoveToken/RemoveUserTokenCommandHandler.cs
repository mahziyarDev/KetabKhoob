using Common.Application;
using Shop.Domain.UserAgg.Repository;

namespace Shop.Application.Users.RemoveToken;

internal class RemoveUserTokenCommandHandler : IBaseCommandHandler<RemoveUserTokenCommand, string>
{
    private readonly IUserRepository _userRepository;

    public RemoveUserTokenCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<OperationResult<string>> Handle(RemoveUserTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetTrackingAsync(request.UserId, cancellationToken);
        if (user == null)
            return OperationResult<string>.NotFound();

        var token = user.RemoveToken(request.TokenId);
        await _userRepository.SaveChangeAsync(cancellationToken);
        return OperationResult<string>.Success(token);
    }
}