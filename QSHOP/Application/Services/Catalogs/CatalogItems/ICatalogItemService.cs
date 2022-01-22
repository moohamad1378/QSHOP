using Common.Dtos;
using Domain.Catalogs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Persistence.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Catalogs.CatalogItems
{
    public interface ICatalogItemService
    {
        int AddCatalogItem(CatalogItemCreadeDto catalogItemCreadeDto);
        List<CatalogItemListDto> ListForAdmin();
        bool Delete(int Id);
        bool Edit(EditCatalogItemDto editCatalogItemDto);
        //
        bool AddColor(CreateColorDto createColorDto);
        List<ColorListDto> ListOfColor(int Id);
        bool EditColor(EditColorDto editColorDto);
        bool DeleteColor(int Id);
        //
        bool AddMaterial(CreateMaterialDto createMaterialDto);
        List<MaterialListDto> ListOfMaterial(int Id);
        bool EditMateriald(EditMaterialDto editMaterialDto);
        bool DeleteMateriald(int Id);
    }
    public class CatalogItemService : ICatalogItemService
    {
        private readonly DataBaseContext _dataBaseContext;
        private readonly IHostingEnvironment _environment;
        public CatalogItemService(DataBaseContext dataBaseContext, IHostingEnvironment environment)
        {
            _dataBaseContext = dataBaseContext;
            _environment = environment;
        }

        public int AddCatalogItem(CatalogItemCreadeDto catalogItemCreadeDto)
        {
            CatalogItem catalogItem = new CatalogItem()
            {
                Name = catalogItemCreadeDto.Name,
                AvailableStock = catalogItemCreadeDto.AvailableStock,
                CatalogBrandId = catalogItemCreadeDto.CatalogBrandId,
                CatalogTypeId = catalogItemCreadeDto.CatalogTypeId,
                DeliveryDetail = catalogItemCreadeDto.DeliveryDetail,
                Description = catalogItemCreadeDto.Description,
                MaxStockThreshold = catalogItemCreadeDto.MaxStockThreshold,
                Price = catalogItemCreadeDto.Price,
                RestockThreshold = catalogItemCreadeDto.RestockThreshold,
                UserId = catalogItemCreadeDto.UserId,
                Detail=catalogItemCreadeDto.Detail,
            };
            _dataBaseContext.CatalogItems.Add(catalogItem);
            _dataBaseContext.SaveChanges();

            var image = new List<Image>();
            foreach (var item in catalogItemCreadeDto.files)
            {
                var uploadet = UploadFile(item);
                image.Add(new Image
                {
                    CataLogItemId = catalogItem.Id,
                    Src = uploadet.FileNameAddress
                });
            }
            _dataBaseContext.Images.AddRange(image);
            _dataBaseContext.SaveChanges();
            return catalogItem.Id;
        }


        public bool Delete(int Id)
        {
            var finded=_dataBaseContext.CatalogItems.SingleOrDefault(p => p.Id == Id);
            _dataBaseContext.CatalogItems.Remove(finded);
            return true;
        }

        public bool Edit(EditCatalogItemDto editCatalogItemDto)
        {
            var data = _dataBaseContext.CatalogItems.SingleOrDefault(p => p.Id == editCatalogItemDto.Id);
            data.AvailableStock = editCatalogItemDto.AvailableStock;
            data.Description = editCatalogItemDto.Description;
            data.DeliveryDetail=editCatalogItemDto.DeliveryDetail;
            data.Detail = editCatalogItemDto.Detail;
            data.Price = editCatalogItemDto.Price;
            data.UserId = editCatalogItemDto.UserId;
            data.Inset = DateTime.Now;
            data.MaxStockThreshold = editCatalogItemDto.MaxStockThreshold;
            data.RestockThreshold = editCatalogItemDto.RestockThreshold;
            data.Name = editCatalogItemDto.Name;
            data.CatalogBrandId = editCatalogItemDto.CatalogBrandId;
            data.CatalogTypeId = editCatalogItemDto.CatalogTypeId;
            _dataBaseContext.SaveChanges();
            return true;
        }

        public List<CatalogItemListDto> ListForAdmin()
        {
            var data = _dataBaseContext.CatalogItems.Select(p => new CatalogItemListDto
            {
                Name = p.Name,
                AvailableStock = p.AvailableStock,
                MaxStockThreshold = p.MaxStockThreshold,
                Id = p.Id,
                Price = p.Price,
                UserId = p.UserId,
                RestockThreshold = p.RestockThreshold
            }).ToList();
            return data;
        }
        #region Color
        public bool AddColor(CreateColorDto createColorDto)
        {
            Color color = new Color
            {
                Name = createColorDto.Name,
                CatalogItemId = createColorDto.CatalogItemId,
            };
            _dataBaseContext.Colors.Add(color);
            _dataBaseContext.SaveChanges();
            return true;
        }
        public List<ColorListDto> ListOfColor(int Id)
        {
            var data = _dataBaseContext.Colors.Where(p => p.CatalogItemId == Id)
                .Select(x=>new ColorListDto
                {
                    Id=x.Id,
                    Name=x.Name,
                    CatalogItemId=x.CatalogItemId,
                }).ToList();
            return data;
        }

        public bool EditColor(EditColorDto editColorDto)
        {
            var data = _dataBaseContext.Colors.SingleOrDefault(p => p.Id == editColorDto.Id);
            data.Name=editColorDto.Name;
            _dataBaseContext.SaveChanges();
            return true;
        }

        public bool DeleteColor(int Id)
        {
            var data = _dataBaseContext.Colors.SingleOrDefault(p => p.Id == Id);
            _dataBaseContext.Colors.Remove(data);
            _dataBaseContext.SaveChanges();
            return true;
        }
        #endregion
        #region Material
        public bool AddMaterial(CreateMaterialDto createMaterialDto)
        {
            Material material = new Material
            {
                CatalogItemId = createMaterialDto.CatalogItemId,
                Name = createMaterialDto.Name,
            };
            _dataBaseContext.Materials.Add(material);
            _dataBaseContext.SaveChanges();
            return true;
        }

        public List<MaterialListDto> ListOfMaterial(int Id)
        {
            var data = _dataBaseContext.Materials.Where(p => p.CatalogItemId == Id)
                .Select(x => new MaterialListDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    CatalogItemId = x.CatalogItemId
                }).ToList();
            return data;
        }

        public bool EditMateriald(EditMaterialDto editMaterialDto)
        {
            var data = _dataBaseContext.Materials.SingleOrDefault(p => p.Id == editMaterialDto.Id);
            data.Name = editMaterialDto.Name;
            _dataBaseContext.SaveChanges();
            return true;
        }

        public bool DeleteMateriald(int Id)
        {
            var data=_dataBaseContext.Materials.SingleOrDefault(p=>p.Id==Id);
            _dataBaseContext.Materials.Remove(data);
            _dataBaseContext.SaveChanges();
            return true;
        }
        #endregion
        private UploadDto UploadFile(IFormFile file)
        {
            if (file != null)
            {

                string folder = $@"images\Item\Picture\";

                var uploadsRootFolder = Path.Combine(_environment.WebRootPath, folder);

                if (!Directory.Exists(uploadsRootFolder))
                {
                    Directory.CreateDirectory(uploadsRootFolder);
                }
                if (file == null || file.Length == 0)
                {
                    return new UploadDto()
                    {
                        Status = false,
                        FileNameAddress = "",

                    };
                }
                string fileName = DateTime.Now.Ticks.ToString() + file.FileName;
                var filePath = Path.Combine(uploadsRootFolder, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                //با دستورات بالا یک عدد به ابتدای فایل اضافه میکند
                {
                    file.CopyTo(fileStream);
                }
                return new UploadDto()
                {
                    FileNameAddress = folder + fileName,
                    Status = true,
                };
            }
            return null;

        }


    }
    #region catalogItem
    public class CatalogItemListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int AvailableStock { get; set; }
        public int RestockThreshold { get; set; }
        public int MaxStockThreshold { get; set; }
        public string UserId { get; set; }
    }
    public class CatalogItemCreadeDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Detail { get; set; }
        public string DeliveryDetail { get; set; }
        public int Price { get; set; }
        public int CatalogTypeId { get; set; }
        public List<IFormFile> files { get; set; }
        public int CatalogBrandId { get; set; }
        public int AvailableStock { get; set; }
        public int RestockThreshold { get; set; }
        public int MaxStockThreshold { get; set; }
        public string? UserId { get; set; }
    }
    public class EditCatalogItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Detail { get; set; }
        public string DeliveryDetail { get; set; }
        public int Price { get; set; }
        public int CatalogTypeId { get; set; }
        public List<IFormFile> files { get; set; }
        public int CatalogBrandId { get; set; }
        public int AvailableStock { get; set; }
        public int RestockThreshold { get; set; }
        public int MaxStockThreshold { get; set; }
        public string? UserId { get; set; }
    }
    #endregion

    #region ColorDtos
    public class CreateColorDto
    {

        public string Name { get; set; }
        public int CatalogItemId { get; set; }
    }
    public class ColorListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CatalogItemId { get; set; }
    }
    public class EditColorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CatalogItemId { get; set; }
    }
    #endregion

    #region Material
    public class EditMaterialDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CatalogItemId { get; set; }
    }

    public class CreateMaterialDto
    {
        public string Name { get; set; }
        public int CatalogItemId { get; set; }
    }


    public class MaterialListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CatalogItemId { get; set; }
    }
    #endregion
}
