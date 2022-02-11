using Domain.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Baskets
{
    public class Basket
    {
        public int Id { get; set; }
        public ICollection<BasketItem> Items { get; set; }=new List<BasketItem>();
        public string BuyerId { get; set; }
        
        public void AddItem(int catalogitemId,int MaterialId ,int UnitPrice,int ColorId, int Quantity)
        {
            var check= Items?.FirstOrDefault(p => p.CatalogItemId == catalogitemId);
            if (check==null)
            {
                Items.Add(new BasketItem
                {
                    UnitPrice = UnitPrice,
                    Quantity = Quantity,
                    CatalogItemId = catalogitemId,
                    MaterialId=MaterialId,
                    ColorId=ColorId
                });
            }
            var Exist = Items.FirstOrDefault(p => p.CatalogItemId == catalogitemId);
            Exist.AddQuantity(Quantity);
        }
    }
    public class BasketItem
    {
        public int Id { get; set; }
        public int UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int ColorId { get; set; }
        public int MaterialId { get; set; }
        public CatalogItem CatalogItem { get; set; }
        public int CatalogItemId { get; set; }
        public int BasketId { get; set; }
        public void AddQuantity(int quantity)
        {
            Quantity += quantity;
        }
        public void SetQuantity(int quantity)
        {

            Quantity = quantity;
        }
    }
}
