using Common.Application;
using Shop.Domain.SellerAgg.Repository;
using Shop.Domain.SellerAgg.Service;

namespace Shop.Application.Sellers.Edit;

/// <summary></summary>
/// <param name="sellerRepository"></param>
/// <param name="sellerDomainService"></param>
public class EditSellerCommandHandler(ISellerRepository sellerRepository, ISellerDomainService sellerDomainService) : IBaseCommandHandler<EditSellerCommand>
{
    private readonly ISellerRepository _sellerRepository = sellerRepository;
    private readonly ISellerDomainService _sellerDomainService = sellerDomainService;
    
    public async Task<OperationResult> Handle(EditSellerCommand request, CancellationToken cancellationToken)
    {
        var seller = await _sellerRepository.GetTrackingAsync(request.Id,cancellationToken);
        if (seller == null)
        {
            return OperationResult.NotFound();
        }
        seller.Edit(request.ShopName,request.NationalCode,request.SellerStatus,_sellerDomainService);
        await _sellerRepository.SaveChangeAsync(cancellationToken);
        return OperationResult.Success();
    }
}