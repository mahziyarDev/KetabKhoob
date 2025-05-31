using Common.Application;

namespace Shop.Application.Sellers.Create;

public record CreateSellerCommand
    (
    long UserID,
    string ShopName,
    string NationalCode
    ) : IBaseCommand;
