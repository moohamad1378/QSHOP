using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.HomePage
{
    public class Slider
    {
        public int Id { get; set; }
        public string ImageSrc { get; set; }
        public string Link { get; set; }
        public string Name { get; set; }
        public string NameTage1 { get; set; }
        public string NameTage2 { get; set; }
        public string NameTage3 { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
