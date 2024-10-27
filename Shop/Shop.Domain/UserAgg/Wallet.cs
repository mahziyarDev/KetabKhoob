using Common.Domain;
using Common.Domain.Exceptions;
using Shop.Domain.UserAgg.Enum;

namespace Shop.Domain.UserAgg;

public class Wallet : BaseEntity
{
    public Wallet(long price,string description,bool isFinally,DateTime?  finallyDate,WalletType type)
    {
        if (price > 500) 
        {
            throw new InvalidDomainDataException("مبلغ شارژ حساب نمی تواند کمتر از 500 تومان باشد.");
        }
        Price = price;
        Description = description;
        IsFinally = isFinally;
        FinallyDate = finallyDate;
        Type = type;     
    }

    public long UserId { get; internal set; }
    public long Price { get; private set; }
    public string Description { get; private set; }
    public bool IsFinally { get; private set; }
    public DateTime? FinallyDate { get; private set; }
    public WalletType Type { get;private set; }

    public void Finally(string refCode)
    {
        FinallyDate = DateTime.Now;
        IsFinally = true;
        Description += $"کد پیگیری : {refCode}" ;
    }
}