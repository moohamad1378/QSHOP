using Domain.Baskets;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Baskets
{
    public interface IBasketService
    {
        BasketDto GetOrCreateBasketForUser(string BuyserID);
        void AddItemToBasket(int BasketId, int catalogitemid, int ColorId, int MaterialId, int quantity = 1);
    }
    public class BasketService : IBasketService
    {
        private readonly DataBaseContext _dataBaseContext;
        public BasketService(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
        }

        public void AddItemToBasket(int BasketId, int catalogitemid, int ColorId , int MaterialId, int quantity = 1)
        {
            var basket = _dataBaseContext.Baskets.FirstOrDefault(p => p.Id == BasketId);
            if(basket == null)
            {

            }
            var catalog = _dataBaseContext.CatalogItems.Find(catalogitemid).Price;
            basket.AddItem(catalogitemid, quantity, catalog,ColorId,MaterialId);
            _dataBaseContext.SaveChanges();
        }

        public BasketDto GetOrCreateBasketForUser(string BuyserID)
        {
            var Basket=_dataBaseContext.Baskets
                .Include(p=>p.Items)
                .ThenInclude(p=>p.CatalogItem)
                .ThenInclude(p=>p.Images).
                SingleOrDefault(p=>p.BuyerId == BuyserID);
            if (Basket == null)
            {
                return CreateBasketForUser(BuyserID);
            }
            return new BasketDto
            {
                Id = Basket.Id,
                BuyerId = Basket.BuyerId,
                Items = Basket.Items.Select(item => new BasketItemDto
                {
                    CatalogItemId = item.CatalogItemId,
                    Id = item.Id,
                    CatalogName = item.CatalogItem.Name,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    IMageUrl = item?.CatalogItem?.Images?.FirstOrDefault()?.Src ?? "",
                }).ToList(),
            };

        }
        private BasketDto CreateBasketForUser(string BuyerId)
        {
            Basket basket = new Basket
            {
                BuyerId = BuyerId
            };
            _dataBaseContext.Baskets.Add(basket);
            _dataBaseContext.SaveChanges();
            return new BasketDto
            {
                BuyerId = basket.BuyerId,
                Id = basket.Id
            };
        }
    }
    public class BasketDto
    {
        public int Id { get; set; }
        public string BuyerId { get; set; }
        public List<BasketItemDto> Items { get; set; }
    }
    public class BasketItemDto
    {
        public int Id { get; set; }
        public int UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int CatalogItemId { get; set; }
        public string CatalogName { get; set; }
        public string IMageUrl { get; set; }
    }
}
