using Domain.HomePage;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.NewsServices
{
    public interface INewsService
    {
        void Add(AddNewsDto addNewsDto);
        List<NewsListDto> GetListForAdmin();
        List<NewsListDto> GetListForSite();
        void Delete(int Id);
        void Edit(EditNewsDto editNewsDto);

    }
    public class NewsService : INewsService
    {
        private readonly DataBaseContext _dataBaseContext;
        public NewsService(DataBaseContext context)
        {
            _dataBaseContext = context;
        }

        public void Add(AddNewsDto addNewsDto)
        {
            News news = new News()
            {
                Description = addNewsDto.Description,
                Title = addNewsDto.Title,
                UserId = addNewsDto.UserId,

            };
            _dataBaseContext.News.Add(news);
            _dataBaseContext.SaveChanges();
        }

        public void Delete(int Id)
        {
            var data = _dataBaseContext.News.FirstOrDefault(p => p.Id == Id);
            _dataBaseContext.News.Remove(data);
            _dataBaseContext.SaveChanges();
        }

        public void Edit(EditNewsDto editNewsDto)
        {
            var data = _dataBaseContext.News.FirstOrDefault(p => p.Id == editNewsDto.Id);
            data.Title = editNewsDto.Title;
            data.Description = editNewsDto.Description;
            _dataBaseContext.SaveChanges();
        }

        public List<NewsListDto> GetListForAdmin()
        {
            PersianCalendar pc = new PersianCalendar();
            var data = _dataBaseContext.News
                ?.Select(p => new NewsListDto
                {
                    Date = pc.GetYear(p.Date).ToString() + "/" + pc.GetMonth(p.Date) + "/" + pc.GetDayOfMonth(p.Date),
                    Description = p.Description,
                    Id = p.Id,
                    Title = p.Title,
                    UserName = _dataBaseContext.Users.FirstOrDefault(x => x.Id == p.UserId).UserName,
                }).ToList();
            return data;
        }

        public List<NewsListDto> GetListForSite()
        {
            PersianCalendar pc = new PersianCalendar();
            var data = _dataBaseContext.News
                ?.Select(p => new NewsListDto
                {
                    Date = pc.GetYear(p.Date).ToString() + "/" + pc.GetMonth(p.Date) + "/" + pc.GetDayOfMonth(p.Date),
                    Description = p.Description,
                    Title = p.Title,
                }).ToList();
            return data;
        }
    }
    public class EditNewsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
    public class NewsListDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public string UserName { get; set; }
    }
    public class AddNewsDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
    }
}
