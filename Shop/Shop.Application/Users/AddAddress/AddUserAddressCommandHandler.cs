using Common.Application;
using Shop.Domain.UserAgg;
using Shop.Domain.UserAgg.Repository;

namespace Shop.Application.Users.AddAddress;

public class AddUserAddressCommandHandler : IBaseCommandHandler<AddUserAddressCommand>
{
    private readonly IUserRepository _userRepository;
    /// <summary></summary>
    /// <param name="userRepository"></param>
    public AddUserAddressCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<OperationResult> Handle(AddUserAddressCommand request, CancellationToken cancellationToken)
    {
        var user =await _userRepository.GetTrackingAsync(request.UserId,cancellationToken);
        if(user==null)
            return OperationResult.NotFound();
        var address = new UserAddress(
            request.Shire,request.City,request.PostalCode,
            request.PostalAddress,request.Name,request.Family,
            request.NationalCode,request.PhoneNumber
            );
        user.AddAddress(address);
        await _userRepository.SaveChangeAsync(cancellationToken);
        return OperationResult.Success();
    }
}