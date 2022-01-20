using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Catalogs
{
    public class Image
    {
        public int Id { get; set; }
        public string Src { get; set; }
        public CatalogItem CataLogItem { get; set; }
        public int CataLogItemId { get; set; }
    }
}
