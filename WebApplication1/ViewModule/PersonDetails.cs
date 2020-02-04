using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Module;

namespace WebApplication1.ViewModule
{
    public class PersonDetails
    {
        public Person Person { get; set; }
        public string FilePath 
        {
            get { return "~/Pictures/" + (Person.FileName ?? "photo.jpg"); }  
        }
    }
}
