using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.ViewModule;

namespace WebApplication1.Module.FileRepositories
{
    public interface IFileRepository
    {
        Person AddPersonFile(AddPerson personWithFile);
        FileInfo RemoveFile(string fileName);
        Person CheckFile(Person person);
        Person UpdatePhoto(AddPerson personWithFile);
    }
}
