using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CardPlatform.Models
{
    public class RegistModel
    {
        [Required(ErrorMessage = "{0}A是必须的")]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "{0}长度不对")]
        [Display(Name = "邮箱地址")]
        [RegularExpression(@"[A-Za-z0-9._]+@[A-Za-z0-9.-]+.[A-Za-z]{2,4}")]
        public string Email { get; set; }

        [StringLength(maximumLength: 20, MinimumLength = 3, ErrorMessage = "{0}长度不对")]
        [Display(Name = "用户名")]
        [Required(ErrorMessage = "{0}A是必须的")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0}A是必须的")]
        [Display(Name = "密码")]
        [StringLength(maximumLength: 20, MinimumLength = 6, ErrorMessage = "{0}长度不对")]
        public string Password { get; set; }

        [Required(ErrorMessage = "{0}A是必须的")]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "两次密码不一样")]
        public string CPassword { get; set; }

    }
}
