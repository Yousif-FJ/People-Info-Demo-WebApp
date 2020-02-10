using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Module;

namespace WebApplication1.ViewModule
{
    public class AddPerson
    {
        [Required]
        public Person Person { get; set; }
        [DataType(DataType.Upload)]
        public IFormFile Picture { get; set; } 
    }
}
