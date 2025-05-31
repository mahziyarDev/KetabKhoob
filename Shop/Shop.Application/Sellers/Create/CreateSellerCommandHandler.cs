using Common.Application;
using Shop.Domain.SellerAgg;
using Shop.Domain.SellerAgg.Repository;
using Shop.Domain.SellerAgg.Service;

namespace Shop.Application.Sellers.Create;

public class CreateSellerCommandHandler : IBaseCommandHandler<CreateSellerCommand>

{
    private readonly ISellerRepository _sellerRepository;
    private readonly ISellerDomainService _domainService;

    /// <summary></summary>
    /// <param name="sellerRepository"></param>
    /// <param name="domainService"></param>
    public CreateSellerCommandHandler(ISellerRepository sellerRepository, ISellerDomainService domainService)
    {
        _sellerRepository = sellerRepository;
        _domainService = domainService;
    }

    public async Task<OperationResult> Handle(CreateSellerCommand request, CancellationToken cancellationToken)
    {
        var seller = new Seller(request.UserID, request.ShopName, request.NationalCode,_domainService);
        await _sellerRepository.AddAsync(seller,cancellationToken);
        await _sellerRepository.SaveChangeAsync(cancellationToken);
        return OperationResult.Success();
    
    }
}
