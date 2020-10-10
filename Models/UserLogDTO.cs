using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CardPlatform.Models
{
    public class UserLogDTO
    {

        [StringLength(maximumLength: 20, MinimumLength = 3, ErrorMessage = "{0}长度不对")]
        [Display(Name = "用户名")]
        [Required(ErrorMessage = "{0}A是必须的")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0}A是必须的")]
        [Display(Name = "密码")]
        [StringLength(maximumLength: 20, MinimumLength = 6, ErrorMessage = "{0}长度不对")]
        public string Password { get; set; }
    }
}
