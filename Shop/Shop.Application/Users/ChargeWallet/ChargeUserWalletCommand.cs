using Common.Application;
using Shop.Domain.UserAgg.Enum;

namespace Shop.Application.Users.ChargeWallet;

/// <summary></summary>
/// <param name="UserId"></param>
/// <param name="Price"></param>
/// <param name="Description"></param>
/// <param name="IsFinally"></param>
/// <param name="Type"></param>
public record ChargeUserWalletCommand
    (
    long UserId,
    int Price,
    string Description,
    bool IsFinally,
    WalletType Type
    )
    : IBaseCommand;
