using AutoMapper;
using Common;
using Common.Resutls;
using Domain.Catalogs;
using Microsoft.EntityFrameworkCore;
using Persistence.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Catalogs.CatalogTypss
{
    public interface ICatalogTypssServices
    {
        ResutlDto<CatalogTypeDto> Add(CatalogTypeDto catalogType);
        ResutlDto Remove(int Id);
        ResutlDto<CatalogTypeDto> Edit(CatalogTypeDto CatalogTypeDto);
        ResutlDto<CatalogTypeDto> FindById(int Id);
        List<CatalogTypeListDto> GetList(int? ParentId);
        List<CatalogTypeListDto> GetListForAdmin();

    }
    public class CatalogTypssServices : ICatalogTypssServices
    {
        private readonly DataBaseContext context;
        private readonly IMapper mapper;

        public CatalogTypssServices(DataBaseContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public ResutlDto<CatalogTypeDto> Add(CatalogTypeDto catalogType)
        {
            var model=mapper.Map<CatalogType>(catalogType);
            context.CatalogTypes.Add(model);
            context.SaveChanges();
            return new ResutlDto<CatalogTypeDto>
            {
                IsSuccess = true
            };
        }

        public ResutlDto<CatalogTypeDto> Edit(CatalogTypeDto catalogTypedto)
        {
            var model = context.CatalogTypes.SingleOrDefault(p => p.Id == catalogTypedto.Id);
            mapper.Map(catalogTypedto, model);
            context.SaveChanges();
            return new ResutlDto<CatalogTypeDto>
            {

                IsSuccess = true,

            };
        }

        public ResutlDto<CatalogTypeDto> FindById(int Id)
        {
            var data = context.CatalogTypes.SingleOrDefault(p => p.Id == Id);
            var result=mapper.Map<CatalogTypeDto>(data);

            return new ResutlDto<CatalogTypeDto>
            {
                Data = result,
                IsSuccess = true,
                
            };
        }

        public List<CatalogTypeListDto> GetList(int? ParentId)
        {
            if(ParentId != null)
            {
                var model1=context.CatalogTypes.Where(p=>p.ParentCatalogTypeId == ParentId)
                    .Select(p=>new CatalogTypeListDto
                    {
                        Id=p.Id,
                        Type=p.Type
                    }).ToList();
                return model1;
            }
            else
            {
                var model = context.CatalogTypes.Where(p => p.ParentCatalogTypeId == null)
                    .Select(p=>new CatalogTypeListDto
                    {
                        Id=p.Id,
                        Type=p.Type
                    }).ToList();

                return model;
            }

        }

        public List<CatalogTypeListDto> GetListForAdmin()
        {
            var types = context.CatalogTypes.Include(p => p.ParentCatalogType)
                .Include(p => p.ParentCatalogType)
                .ThenInclude(p => p.ParentCatalogType.ParentCatalogType)
                .Include(p => p.SubType).
                Where(p => p.ParentCatalogTypeId == null)
                .Where(p => p.SubType.Count == 0)
                .Select(p => new { p.Id, p.Type, p.ParentCatalogType, p.SubType }).ToList()
                .Select(p => new CatalogTypeListDto
                {
                    Id = p.Id,
                    Type = $"{p?.Type ?? ""}-{p?.ParentCatalogType?.Type ?? ""} - {p?.ParentCatalogType?.Type ?? ""}"
                }).ToList();
            return types;
        }

        public ResutlDto Remove(int Id)
        {
            var catalogtype = context.CatalogTypes.Where(p=>p.Id==Id || p.ParentCatalogTypeId==Id).ToList();
            context.CatalogTypes.RemoveRange(catalogtype);
            context.SaveChanges();
            return new ResutlDto
            {
                IsSuccess = true,
            };
        }
    }
    public class CatalogTypeDto
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public int? ParentCatalogTypeId { get; set; }

    }
    public class CatalogTypeListDto
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public int SubTypeCount { get; set; }
    }
}
