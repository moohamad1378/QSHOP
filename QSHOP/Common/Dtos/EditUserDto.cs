using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dtos
{
    public class EditUserDto
    {
        public string Id { get; set; }
        [Required]
        [Display(Name = "نام")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "ایمیل")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
