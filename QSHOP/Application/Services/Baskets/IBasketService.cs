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
        void DeleteBasketItem(int Id);
        bool SetQuantity(int CatalogItemId, int quantity);
        BasketDto GetBasketForUser(string UserId);
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
            var UnitPrice = _dataBaseContext.CatalogItems.Find(catalogitemid).Price;
            basket.AddItem(catalogitemid, MaterialId, UnitPrice, ColorId,quantity);
            _dataBaseContext.SaveChanges();
        }

        public void DeleteBasketItem(int Id)
        {
            var data = _dataBaseContext.BasketItems.FirstOrDefault(p => p.Id == Id);
            _dataBaseContext.BasketItems.Remove(data);
            _dataBaseContext.SaveChanges();
        }

        public BasketDto GetBasketForUser(string UserId)
        {
            var Basket = _dataBaseContext.Baskets
    .Include(p => p.Items)
    .ThenInclude(p => p.CatalogItem)
    .ThenInclude(p => p.Images)
    .SingleOrDefault(p => p.BuyerId == UserId);
            if (Basket == null)
            {
                return null;
            }
            foreach (var item in Basket.Items)
            {
                var calorname = _dataBaseContext.Colors.FirstOrDefault(p => p.Id == item.ColorId).Name;

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
                    ColorName = _dataBaseContext.Colors?.FirstOrDefault(p => p.Id == item.ColorId)?.Name,
                    MaterialName = _dataBaseContext.Materials?.FirstOrDefault(p => p.Id == item.MaterialId)?.Name,
                }).ToList(),
            };
        }

        public BasketDto GetOrCreateBasketForUser(string BuyserID)
        {
            var Basket=_dataBaseContext.Baskets
                .Include(p=>p.Items)
                .ThenInclude(p=>p.CatalogItem)
                .ThenInclude(p=>p.Images)
                .SingleOrDefault(p=>p.BuyerId == BuyserID);
            if (Basket == null)
            {
                return CreateBasketForUser(BuyserID);
            }
            foreach (var item in Basket.Items)
            {
                var calorname = _dataBaseContext.Colors.FirstOrDefault(p => p.Id == item.ColorId).Name;
                
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
                    ColorName= _dataBaseContext.Colors?.FirstOrDefault(p => p.Id == item.ColorId)?.Name,
                    MaterialName= _dataBaseContext.Materials?.FirstOrDefault(p => p.Id == item.MaterialId)?.Name,
                }).ToList(),
            };

        }

        public bool SetQuantity(int CatalogItemId, int quantity)
        {
            var item = _dataBaseContext.BasketItems.SingleOrDefault(p => p.Id == CatalogItemId);
            item.SetQuantity(quantity);
            _dataBaseContext.SaveChanges();
            return true;
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
        public string MaterialName { get; set; }
        public string ColorName { get; set; }
        public int CatalogItemId { get; set; }
        public string CatalogName { get; set; }
        public string IMageUrl { get; set; }
    }
}
