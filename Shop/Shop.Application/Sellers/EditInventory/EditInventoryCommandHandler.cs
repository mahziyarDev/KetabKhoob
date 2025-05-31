using Common.Application;
using Shop.Domain.SellerAgg.Repository;

namespace Shop.Application.Sellers.EditInventory;

public class EditInventoryCommandHandler : IBaseCommandHandler<EditInventoryCommand>
{
    private readonly ISellerRepository _sellerRepository;
    /// <summary></summary>
    /// <param name="sellerRepository"></param>
    public EditInventoryCommandHandler(ISellerRepository sellerRepository)
    {
        _sellerRepository = sellerRepository;
    }

    public async Task<OperationResult> Handle(EditInventoryCommand request, CancellationToken cancellationToken)
    {
        var seller = await _sellerRepository.GetTracking(request.SellerId,cancellationToken);
        if (seller == null)
            return OperationResult.NotFound();
        //این مورد در دامین چک شده است
        // if(seller.Inventories.Count == 0 || seller.Inventories.Any(x=>x.Id == request.InventoryId) == false)
        //     return OperationResult.NotFound();
        seller.EditInventory(request.InventoryId,request.Price,request.Count,request.DiscountPercentage);
        await _sellerRepository.SaveChangeAsync(cancellationToken);
        return OperationResult.Success();
    }
}