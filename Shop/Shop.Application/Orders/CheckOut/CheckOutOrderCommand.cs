using Common.Application;

namespace Shop.Application.Orders.CheckOut;

public record CheckOutOrderCommand
(
    long UserId,
    string Shire,
    string City,
    string PostalCode,
    string PostalAddress,
    string PhoneNumber,
    string Name,
    string Family,
    string NationalCode
): IBaseCommand;