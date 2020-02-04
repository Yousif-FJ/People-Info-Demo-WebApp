using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Module
{
    public class Person
    {

        [Required]
        [StringLength(10)]
        [MinLength(2)]
        public string Name { get; set; }
        public int ID { get; set; }
        public Sector Sector { get; set; }
        [Range (12,70)]
        public int Age { get; set; }
        public string FileName { get; set;}
    }
    
    public  enum Sector
    {
        IT,HR,Div
    }
}

