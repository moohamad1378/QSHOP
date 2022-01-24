using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Catalogs;

namespace Application.Services.Catalogs.System.SystemServices
{
    public interface ISystemServices
    {
        bool Add(CreateSystemDto createSystemDto);
        List<ListSystemDto> List();
        bool Edit(EditSystemDto editSystemDto);
        bool Delete(int id);
    }
    public class SystemServices : ISystemServices
    {
        private readonly DataBaseContext _dataBaseContext;
        public SystemServices(DataBaseContext dataBaseContext)
        {
            _dataBaseContext= dataBaseContext;
        }
        public bool Add(CreateSystemDto createSystemDto)
        {
            Domain.Catalogs.System system = new Domain.Catalogs.System()
            {
                Name = createSystemDto.Name,
            };
            _dataBaseContext.Systems.Add(system);
            _dataBaseContext.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var data=_dataBaseContext.Systems.SingleOrDefault(p=>p.Id==id);
            _dataBaseContext.Systems.Remove(data);
            _dataBaseContext.SaveChanges();
            return true;
        }

        public bool Edit(EditSystemDto editSystemDto)
        {
            var data = _dataBaseContext.Systems.SingleOrDefault(p => p.Id == editSystemDto.Id);
            data.Name = editSystemDto.Name;
            _dataBaseContext.SaveChanges();
            return true;
        }

        public List<ListSystemDto> List()
        {
            var data = _dataBaseContext.Systems
                .Select(p => new ListSystemDto
                {
                    Id = p.Id,
                    Name = p.Name,
                })?.ToList();
            return data;
        }
    }
    public class CreateSystemDto
    {
        public string Name { get; set; }
    }
    public class ListSystemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class EditSystemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
