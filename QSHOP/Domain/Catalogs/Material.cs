using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Catalogs
{
    public class Material
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual CatalogItem CatalogItem { get; set; }
        public int CatalogItemId { get; set; }
    }
}
