﻿namespace Domain.Catalogs
{
    public class CatalogItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Detail { get; set; }
        public string DeliveryDetail { get; set; }
        public int Price { get; set; }
        public CatalogType CatalogType { get; set; }
        public int CatalogTypeId { get; set; }

        public CatalogBrand CatalogBrand { get; set; }
        public int CatalogBrandId    { get; set; }
        public int AvailableStock { get; set; }
        //برای اخطار به ادمین کم شدن موجودی از یه حدی
        public int RestockThreshold { get; set; }
        public int MaxStockThreshold { get; set; }
        public DateTime Inset { get; set; } = DateTime.Now;
        public string UserId { get; set; }
        public ICollection<Color> Colors { get; set; }
        public int ColorsId { get; set; }
        public ICollection<Material> Materials { get; set; }
        public int MaterialsId { get; set; }
    }
}
