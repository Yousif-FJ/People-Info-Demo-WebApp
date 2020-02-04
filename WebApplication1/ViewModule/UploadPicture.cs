using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.ViewModule
{
    public class UploadPicture
    {
        public IFormFile FormFile { get; set; }
        public int PersonID { get; set; }
        public string FileName { get; set; }
        public string FilePath
        {
            get { return "~/Pictures/" + (FileName ?? "photo.jpg"); }
        }
    }
}
