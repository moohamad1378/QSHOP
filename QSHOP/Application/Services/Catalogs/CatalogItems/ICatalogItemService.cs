using Common.Dtos;
using Domain.Catalogs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Resutls;
using Application.Dto;

namespace Application.Services.Catalogs.CatalogItems
{
    public interface ICatalogItemService
    {
        int AddCatalogItem(CatalogItemCreadeDto catalogItemCreadeDto);
        PaginatedItemsDto<CatalogItemListDto> ListForAdmin(CatlogPLPRequestDto request);
        bool Delete(int Id);
        bool Edit(EditCatalogItemDto editCatalogItemDto);
        DetailProductDto Detail(int Id,string Slug);
        void IsViZhe(int Id);
        List<viZheDto> getVizhe();

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
                Size = catalogItemCreadeDto.Size,
                MaterialsId=catalogItemCreadeDto.MaterialsId,
                Slug=catalogItemCreadeDto.Slug,
                SystemId=catalogItemCreadeDto.SystemId,
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
            _dataBaseContext.SaveChanges();
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

        public PaginatedItemsDto<CatalogItemListDto> ListForAdmin(CatlogPLPRequestDto request)
        {
            int rowcount = 0;
            var query = _dataBaseContext.CatalogItems
                .Include(p => p.Images)
                .OrderByDescending(p => p.Id)
                .Where(p=>p.AvailableStock >0)
                .AsQueryable();

            if (request.brandId != null)
            {
                query = query.Where(p => request.brandId.Any(b => b == p.CatalogBrandId));
            }

            if (request.CatalogTypeId != null)
            {
                query = query.Where(p => p.CatalogTypeId == request.CatalogTypeId);
            }
            if (request.SystemId != null)
            {
                query = query.Where(p => request.SystemId.Any(b => b == p.SystemId));
            }

            if (!string.IsNullOrEmpty(request.SearchKey))
            {
                query = query.Where(p => p.Name.Contains(request.SearchKey)
                || p.Description.Contains(request.SearchKey));
            }

            if (request.AvailableStock == true)
            {
                query = query.Where(p => p.AvailableStock > 0);
            }

            if (request.SortType == SortType.newest)
            {
                query = query
                    .OrderByDescending(p => p.Id);
            }

            if (request.SortType == SortType.cheapest)
            {
                query = query
                    .OrderBy(p => p.Price);
            }

            if (request.SortType == SortType.mostExpensive)
            {
                query = query
                    .OrderByDescending(p => p.Price);
            }

            if (request.Size != null && request.Size !=Size.Non)
            {
                query = query.Where(p => p.Size == request.Size);
            }


            var data = query.PagedResult(request.page, request.pageSize=10, out rowcount)
                .ToList()
                .Select(p => new CatalogItemListDto
                {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                ImageSrc = p.Images?.FirstOrDefault()?.Src,
                AvailableStock = p.AvailableStock,
                Slug=p.Slug,
                UserId=p.UserId,
                IsVizhe=p.IsVizhe
                })?.ToList();
            return new PaginatedItemsDto<CatalogItemListDto>(request.page, request.pageSize, rowcount, data);
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

        public DetailProductDto Detail(int Id, string Slug)
        {
            
            var data = _dataBaseContext.CatalogItems
                .Include(p=>p.Images)
                .Include(p=>p.Materials)
                .Include(p=>p.Colors)
                .SingleOrDefault(p => p.Id == Id);
            var system = _dataBaseContext.Systems.SingleOrDefault(p => p.Id == data.SystemId);
            data.Visit++;
            _dataBaseContext.SaveChanges();
            DetailProductDto dto = new DetailProductDto
            {
                Id = Id,
                AvailableStock = data.AvailableStock,
                DeliveryDetail = data.DeliveryDetail,
                Description = data.Description,
                Detail = data.Detail,
                Name = data.Name,
                Price = data.Price,
                Size = data.Size,
                Systemname = system.Name,

                Src = data.Images.Select(p => new Images
                {
                    Id = p.Id,
                    ImageSrc = p.Src
                }).ToList(),
                DetailColorDtos = data.Colors.Select(p => new DetailColorDto
                {
                    Id = p.Id,
                    Name = p.Name
                }).ToList(),
                DetailMaterialDtos = data.Materials.Select(p => new DetailMaterialDto
                {
                    Id = p.Id,
                    Name = p.Name
                }).ToList(),

            };
            return dto;
        }

        public void IsViZhe(int Id)
        {
            var data = _dataBaseContext.CatalogItems.FirstOrDefault(p => p.Id == Id);
            if(data.IsVizhe == true)
            {
                data.IsVizhe = false;
                _dataBaseContext.SaveChanges();
                return;
            }
            if(data.IsVizhe == false)
            {
                data.IsVizhe= true;
                _dataBaseContext.SaveChanges();
                return;
            }

        }

        public List<viZheDto> getVizhe()
        {
            var data = _dataBaseContext.CatalogItems
                .Include(p => p.Images)
                .Where(p => p.IsVizhe == true)
                .Select(p => new viZheDto
                {
                    Dec = p.Description,
                    Id = p.Id,
                    ImageSrc = p.Images.FirstOrDefault().Src,
                    Slug = p.Slug,
                    Price=p.Price
                }).ToList();
            return data;
        }
    }
    public class viZheDto
    {
        public int Id { get; set; }
        public string Slug { get; set; }
        public string ImageSrc { get; set; }
        public string Dec { get; set; }
        public int Price { get; set; }
    }
    public class CatlogPLPRequestDto
    {
        public int page { get; set; } = 1;
        public int pageSize { get; set; } = 2;
        public int? CatalogTypeId { get; set; }
        public int[] brandId { get; set; }
        public int[] SystemId { get; set; }
        public bool AvailableStock { get; set; }
        public string SearchKey { get; set; }
        public Size Size { get; set; }
        public SortType SortType { get; set; }
    }
    public enum SortType
    {
        /// <summary>
        /// بدونه مرتب سازی
        /// </summary>
        None = 0,
        /// <summary>
        ///  ‌جدیدترین
        /// </summary>
        newest = 4,
        /// <summary>
        /// ارزان‌ترین
        /// </summary>
        cheapest = 5,
        /// <summary>
        /// گران‌ترین
        /// </summary>
        mostExpensive = 6,
    }
    #region catalogItem
    public class CatalogItemListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public int Price { get; set; }
        public int AvailableStock { get; set; }
        public int RestockThreshold { get; set; }
        public int MaxStockThreshold { get; set; }
        public string UserId { get; set; }
        public string ImageSrc { get; set; }
        public bool IsVizhe { get; set; }

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
        public Size Size { get; set; }
        public int MaterialsId { get; set; }
        public int SystemId { get; set; }
        public string Slug { get; set; }
    }
    public class DetailProductDto
    {
        public long View { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Detail { get; set; }
        public string DeliveryDetail { get; set; }
        public int Price { get; set; }
        public int CatalogTypeId { get; set; }
        public List<Images> Src { get; set; }
        public List<DetailMaterialDto> DetailMaterialDtos { get; set; }
        public List<DetailColorDto> DetailColorDtos{ get; set; }
        public int CatalogBrandId { get; set; }
        public int AvailableStock { get; set; }
        public int RestockThreshold { get; set; }
        public int MaxStockThreshold { get; set; }
        public Size Size { get; set; }
        public int MaterialsId { get; set; }
        public string Systemname { get; set; }
        public string Slug { get; set; }
    }
    public class Images
    {
        public int Id { get; set; }
        public string ImageSrc { get; set; }
    }
    public class DetailMaterialDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class DetailColorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
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
