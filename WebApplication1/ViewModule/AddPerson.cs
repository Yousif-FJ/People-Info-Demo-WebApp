using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Module;
using WebApplication1.ValidationAttributes;

namespace WebApplication1.ViewModule
{
    public class AddPerson
    {
        [Required]
        public Person Person { get; set; }

        [DataType(DataType.Upload)]
        [AlowPicture]
        [MaxFileSize(102400)]
        public IFormFile Picture { get; set; } 
    }
}
