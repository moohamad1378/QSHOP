using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dtos
{
    public class UserListDto
    {
        public string Id { get; set; }
        [Display(Name ="نام کاربری")]
        public string UserName { get; set; }
        [Display(Name ="ایمیل")]
        public string Email { get; set; }
        public string roleid { get; set; }
    }
}
