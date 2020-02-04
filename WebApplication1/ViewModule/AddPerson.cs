using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Module;

namespace WebApplication1.ViewModule
{
    public class AddPerson
    {
        public Person Person { get; set; }
        public IFormFile Picture { get; set; } 
    }
}
