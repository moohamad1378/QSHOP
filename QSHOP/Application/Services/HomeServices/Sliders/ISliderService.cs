using Common.Dtos;
using Domain.HomePage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.HomeServices.Sliders
{
    public interface ISliderService
    {
        void Add(AddSliderDto addSliderDto);
        List<ListSliderDto> GetList();
        void Delete(int Id);
    }
    public class SliderService : ISliderService
    {
        private readonly DataBaseContext _dataBaseContext;
        private readonly IHostingEnvironment _environment;
        public SliderService(DataBaseContext dataBaseContext, IHostingEnvironment environment)
        {
            _dataBaseContext = dataBaseContext;
            _environment = environment;
        }

        public void Add(AddSliderDto addSliderDto)
        {
            Slider slider = new Slider()
            {
                ImageSrc = UploadFile(addSliderDto.File).FileNameAddress,
                Name = addSliderDto.Name,
                Link = addSliderDto.Link,
                NameTage1=addSliderDto.NameTage1,
                NameTage2=addSliderDto.NameTage2,
                NameTage3=addSliderDto.NameTage3,
            };
            _dataBaseContext.Sliders.Add(slider);
            _dataBaseContext.SaveChanges();
        }

        public void Delete(int Id)
        {
            var data = _dataBaseContext.Sliders.FirstOrDefault(p => p.Id == Id);
            data.IsActive=false;
            _dataBaseContext.SaveChanges();
        }

        public List<ListSliderDto> GetList()
        {
            var data = _dataBaseContext.Sliders.Where(p => p.IsActive == true && p.Link != null)
                .Select(p => new ListSliderDto
                {
                    Link = p.Link,
                    ImageSrc = p.ImageSrc,
                    Id=p.Id,
                    Name=p.Name,
                    NameTage1=p.NameTage1,
                    NameTage2=p.NameTage2,
                    NameTage3=p.NameTage3
                })?.ToList();
            return data;
        }

        private UploadDto UploadFile(IFormFile file)
        {
            if (file != null)
            {

                string folder = $@"images\Slider\Picture\";

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
    public class ListSliderDto
    {
        public int Id { get; set; }
        public string ImageSrc { get; set; }
        public string Link { get; set; }
        public string Name { get; set; }
        public string NameTage1 { get; set; }
        public string NameTage2 { get; set; }
        public string NameTage3 { get; set; }
    }
    public class AddSliderDto
    {
        public string Name { get; set; }
        public string NameTage1 { get; set; }
        public string NameTage2 { get; set; }
        public string NameTage3 { get; set; }
        public IFormFile File { get; set; }
        public string Link { get; set; }
    }
}
