using AutoMapper;
using Common;
using Common.Resutls;
using Domain.Catalogs;
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
        PaginatedItemsDto<CatalogTypeListDto> GetList(int? ParentId, int page, int pagesize);
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
                Data = mapper.Map<CatalogTypeDto>(model),
                IsSuccess = true,
                Message = { $"تایپ {model.Type} با موفقیت ثبت شد" }
            };
        }

        public ResutlDto<CatalogTypeDto> Edit(CatalogTypeDto catalogTypedto)
        {
            var model = context.CatalogTypes.SingleOrDefault(p => p.Id == catalogTypedto.Id);
            mapper.Map(catalogTypedto, model);
            context.SaveChanges();
            return new ResutlDto<CatalogTypeDto>
            {
                Data = mapper.Map<CatalogTypeDto>(model),
                IsSuccess = true,
                Message = { $"تایپ {model.Type} با موفقیت ویرایش شد" }
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

        public PaginatedItemsDto<CatalogTypeListDto> GetList(int? ParentId, int page, int pagesize)
        {
            int totalcount=0;
            var model=context.CatalogTypes.AsQueryable().PagedResult(page,pagesize,out totalcount);
            var result = mapper.ProjectTo<CatalogTypeListDto>(model).ToList();
            return new PaginatedItemsDto<CatalogTypeListDto>(page,pagesize,totalcount,result);
        }

        public ResutlDto Remove(int Id)
        {
            var catalogtype = context.CatalogTypes.Find(Id);
            context.CatalogTypes.Remove(catalogtype);
            context.SaveChanges();
            return new ResutlDto
            {
                IsSuccess = true,
                Message = { " حذف شد" }
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
        public int Id { get;}
        public string? Type { get; set; }
        public int SubTypeCount { get; set; }
    }
}
