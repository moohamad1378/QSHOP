using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Resutls
{
    public class ResutlDto<T>
    {
        public bool IsSuccess { get; set; }
        public List<string>? Message { get; set; }
        public T? Data { get; set; }
    }
    public class ResutlDto
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}
