using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.ValidationAttributes;

namespace WebApplication1.ViewModule
{
    public class UploadPicture
    {
        [Required(ErrorMessage ="Select photo")]
        [DataType(DataType.Upload)]
        [AlowPicture]
        [MaxFileSize(102400)]
        public IFormFile FormFile { get; set; }
        [Required]
        public int PersonID { get; set; }
        public string FileName { get; set; }
        public string FilePath
        {
            get { return "~/Pictures/" + (FileName ?? "photo.jpg"); }
        }
    }
}
