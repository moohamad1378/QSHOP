using Common.Dtos;
using Domain.HomePage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.OurBrandService
{
    public interface IOurBrandService
    {
        void Add(AddOurBrandDto addOurBrandDto);
        List<ListOurBrandDto> List();
        void Delete(int id);
    }
    public class OurBrandService : IOurBrandService
    {
        private readonly DataBaseContext _dataBaseContext;
        private readonly IHostingEnvironment _environment;
        public OurBrandService(DataBaseContext dataBaseContext,
            IHostingEnvironment environment)
        {
            _dataBaseContext = dataBaseContext;
            _environment = environment;
        }

        public void Add(AddOurBrandDto addOurBrandDto)
        {
            OurBrands ourBrands = new OurBrands
            {
                Src = UploadFile(addOurBrandDto.File).FileNameAddress
            };
            _dataBaseContext.OurBrands.Add(ourBrands);
            _dataBaseContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var data=_dataBaseContext.OurBrands.FirstOrDefault(p=>p.Id== id);
            _dataBaseContext.OurBrands.Remove(data);
            _dataBaseContext.SaveChanges();
        }

        public List<ListOurBrandDto> List()
        {
            var data = _dataBaseContext.OurBrands.Select(p => new
              ListOurBrandDto
            {
                Id = p.Id,
                Src = p.Src
            }).ToList();
            return data;
        }

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
    public class ListOurBrandDto
    {
        public int Id { get; set; }
        public string Src { get; set; }
    }
    public class AddOurBrandDto
    {
        public IFormFile File { get; set; }
    }
}
