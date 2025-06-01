using Common.Application;
using Shop.Domain.UserAgg;
using Shop.Domain.UserAgg.Repository;

namespace Shop.Application.Users.EditAddress;

internal class EditUserAddressCommandHandler : IBaseCommandHandler<EditUserAddressCommand>
{
    private readonly IUserRepository _userRepository;
    /// <summary></summary>
    /// <param name="userRepository"></param>
    public EditUserAddressCommandHandler(IUserRepository userRepository) => _userRepository = userRepository;

    public async Task<OperationResult> Handle(EditUserAddressCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetTracking(request.UserId,cancellationToken);
        if (user == null)
            return OperationResult.NotFound();

        var address = new UserAddress(request.Shire, request.City, request.PostalCode, request.PostalAddress,
             request.Name, request.Family, request.NationalCode,request.PhoneNumber);

        user.EditAddress(address, request.Id);
        await _userRepository.SaveChangeAsync(cancellationToken);

        return OperationResult.Success();
    }
}