using Common.Application;
using Shop.Domain.SellerAgg;
using Shop.Domain.SellerAgg.Repository;

namespace Shop.Application.Sellers.AddInventory;

public class AddInventoryCommandHandler : IBaseCommandHandler<AddInventoryCommand>
{
    private readonly ISellerRepository _sellerRepository;
    /// <summary></summary>
    /// <param name="sellerRepository"></param>
    public AddInventoryCommandHandler(ISellerRepository sellerRepository)
    {
        _sellerRepository = sellerRepository;
    }

    public async Task<OperationResult> Handle(AddInventoryCommand request, CancellationToken cancellationToken)
    {
        var seller = await _sellerRepository.GetTrackingAsync(request.SellerId,cancellationToken);
        if (seller == null)
            return OperationResult.NotFound();
        var inventory =
            new SellerInventory(request.ProductId, request.Count, request.Price, request.DiscountPercentage);
        seller.AddInventory(inventory);
        await _sellerRepository.SaveChangeAsync(cancellationToken);
        return OperationResult.Success();
    }
}