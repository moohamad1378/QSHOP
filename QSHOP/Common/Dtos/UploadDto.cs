using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dtos
{
    public class UploadDto
    {
        public long Id { get; set; }
        public bool Status { get; set; }
        public string FileNameAddress { get; set; }
    }
}
