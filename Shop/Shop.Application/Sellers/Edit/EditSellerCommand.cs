using Common.Application;
using Shop.Domain.SellerAgg.Enum;

namespace Shop.Application.Sellers.Edit;
/// <summary></summary>
/// <param name="Id"></param>
/// <param name="ShopName"></param>
/// <param name="NationalCode"></param>
public record EditSellerCommand 
    (
        long Id,
        string ShopName,
        SellerStatus SellerStatus,
        string NationalCode
    )
    : IBaseCommand;