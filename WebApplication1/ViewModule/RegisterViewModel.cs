using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Controllers;

namespace WebApplication1.ViewModule
{
    public class RegisterViewModel
    {
        [Required]
        [MaxLength(12)]
        [DataType(DataType.Text)]
        [Remote(nameof(AccountController.IsUserNameAvailable),"Account")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public bool PresistantLogin { get; set; }
    }
}
