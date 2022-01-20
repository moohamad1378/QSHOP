using Common.Resutls;
using Domain.Catalogs;
using Persistence.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Catalogs.CatalogBrands
{
    public interface ICatalogBrandService
    {
        ResutlDto Add(CatalogBranCreateDto catalogBranCreateDto);
        bool Delete(int Id);
        FIndCatalogBrand Find(int Id);
        bool Edit(FIndCatalogBrand fIndCatalogBrand);
        List<CatalogBrandList> List();
    }
    public class CatalogTypeService: ICatalogBrandService
    {
        private readonly DataBaseContext _context;
        public CatalogTypeService(DataBaseContext context)
        {
            _context = context;
        }

        public ResutlDto Add(CatalogBranCreateDto catalogBranCreateDto)
        {
            var Compare=_context.CatalogBrands.SingleOrDefault(p=>p.Brand==catalogBranCreateDto.Brand);
            if (Compare == null)
            {
                CatalogBrand brand = new CatalogBrand()
                {
                    Brand = catalogBranCreateDto.Brand,
                };
                _context.CatalogBrands.Add(brand);
                _context.SaveChanges();
                return new ResutlDto
                {
                    IsSuccess = true,
                };
            }
            else
            {
                return new ResutlDto
                {
                    IsSuccess = false,
                    Message = "این نام قبلا ثبت شده است"
                };
            }
        }

        public bool Delete(int Id)
        {
            var data = _context.CatalogBrands.SingleOrDefault(p => p.Id == Id);

            if (data != null)
            {
                _context.CatalogBrands.Remove(data);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Edit(FIndCatalogBrand fIndCatalogBrand)
        {
            var data=_context.CatalogBrands.SingleOrDefault(p=>p.Id==fIndCatalogBrand.Id);
            data.Brand= fIndCatalogBrand.Brand;
            _context.SaveChanges();
            return true;
        }

        public FIndCatalogBrand Find(int Id)
        {
            var data = _context.CatalogBrands.Where(p => p.Id == Id)
                .Select(p=> new FIndCatalogBrand
                {
                    Id=p.Id,
                    Brand=p.Brand
                })?.SingleOrDefault();
            return data;
            
        }

        public List<CatalogBrandList> List()
        {
            var data = _context.CatalogBrands.Where(p => p.Brand != null)
                .Select(p => new CatalogBrandList
                {
                    Id = p.Id,
                    Brand = p.Brand
                }).ToList();
            return data;
        }
    }
    public class CatalogBranCreateDto
    {
        public string? Brand { get; set; }
    }
    public class FIndCatalogBrand
    {
        public int Id { get; set; }
        public string? Brand { get; set; }
    }
    public class CatalogBrandList
    {
        public int Id { get; set; }
        public string? Brand { get; set; }
    }
}
