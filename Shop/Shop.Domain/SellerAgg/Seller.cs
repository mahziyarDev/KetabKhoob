using Common.Domain;
using Common.Domain.Exceptions;
using Shop.Domain.SellerAgg.Enum;
using Shop.Domain.SellerAgg.Service;

namespace Shop.Domain.SellerAgg;

public class Seller : AggregateRoot
{
    
    #region Properties

        public long UserID { get; private set; }
        public string ShopName { get; private set; }
        public string NationalCode { get; private set; }
        public SellerStatus SellerStatus { get; private set; }
        public List<SellerInventory> Inventories { get; private set; }
        public DateTime? LastUpdate { get; private set; }

    #endregion
    
    #region Function

        private Seller(){}
        
        public Seller(long userId, string shopName, string nationalCode, ISellerDomainService domainService)
        { 
            Gurad(shopName,nationalCode);
            UserID = userId;
            ShopName = shopName;
            NationalCode = nationalCode;
            Inventories = new List<SellerInventory>();
            if(domainService.IsValidSellerInformation(this) == false)
                throw new InvalidDomainDataException("اطلاعات نامعتبر است");
        }
        public void ChangeStatus(SellerStatus sellerStatus)
        {
            SellerStatus = sellerStatus;       
            LastUpdate = DateTime.Now;
        }

        public void Edit(string shopName, string nationalCode,SellerStatus sellerStatus, ISellerDomainService domainService)
        {
            Gurad(shopName,nationalCode);
            if (nationalCode != NationalCode)
            {
                if (domainService.NationalCodeExistInDataBase(nationalCode))
                {
                    throw new InvalidDomainDataException("کد ملی متعلق به شخص دیگری است");
                }
            }
            ShopName = shopName;
            NationalCode = nationalCode;
            SellerStatus = sellerStatus;
        }

        public void AddInventory(SellerInventory inventory)
        {
            if (Inventories.Any(x => x.ProductId == inventory.ProductId))
                throw new InvalidDomainDataException("این محصول قبلا ثبت شده است");
            Inventories.Add(inventory);
        }

    
        // public void EditInventory(SellerInventory inventory)
        // {
        //     var currentInventory = Inventories.FirstOrDefault(x => x.Id == inventory.Id);
        //     if (currentInventory == null)
        //         return;
        //     
        //     Inventories.Remove(currentInventory);
        //     Inventories.Add(inventory);
        // }
        //
        public void EditInventory(long inventoryId,int price,int count,int? discountPercentage)
        {
            var currentInventory = Inventories.FirstOrDefault(x => x.Id == inventoryId);
            if (currentInventory == null)
                throw new InvalidDomainDataException("محصول یافت نشد.");
            //TODO : Check Inventory
            currentInventory.Edit(count,price,discountPercentage);
            
        }
        public void DeleteInventory(long inventoryId)
        {
            var currentInventory = Inventories.FirstOrDefault(x => x.Id == inventoryId);
            if (currentInventory == null)
                throw new InvalidDomainDataException("محصول یافت نشد.");
            
            Inventories.Remove(currentInventory);
        }

        private void Gurad(string shopName, string nationalCode)
        {
            NullOrEmptyDomainDataException.CheckString(shopName,nameof(shopName));
            NullOrEmptyDomainDataException.CheckString(nationalCode,nameof(nationalCode));

            if (IranianNationalIdChecker.IsValid(nationalCode) == false)
                throw new InvalidDomainDataException("کدملی نامعتبر است.");
        }
        
    #endregion
}