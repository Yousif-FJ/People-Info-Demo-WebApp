﻿using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.ViewModule;

namespace WebApplication1.Module.FileRepositories
{
    public class RootFileRepository : IFileRepository
    {
        private readonly string FileConatiner ;

        public RootFileRepository(IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment is null) { throw new ArgumentNullException(nameof(webHostEnvironment)); };
            FileConatiner = Path.Combine(webHostEnvironment.WebRootPath, "Pictures");
        }

        public Person AddPersonFile(AddPerson personWithFile)
        {
            if (personWithFile is null)
            {
                throw new ArgumentNullException(nameof(personWithFile));
            }
            Person person = personWithFile.Person;
            if (personWithFile.Picture is null) { return person; }
            string PictureUniqeFile = Path.Combine(FileConatiner, Guid.NewGuid().ToString() + "_" + personWithFile.Picture.FileName);
            using FileStream fileStream = new FileStream(PictureUniqeFile, FileMode.CreateNew);
            personWithFile.Picture.CopyTo(fileStream);
            person.FileName = Path.GetFileName(PictureUniqeFile);

            return person;
        }

        public Person CheckFile(Person person)
        {
            if (person is null)
            {
                throw new ArgumentNullException(nameof(person));
            }
            if (person.FileName!=null)
            {
                string FilePath = Path.Combine(FileConatiner, person.FileName);
                FileInfo file = new FileInfo(FilePath);
                if (!file.Exists)
                {
                    person.FileName = null;
                } 
            }
            return person;
        }


        public FileInfo RemoveFile(string fileName)
        {
            if (fileName is null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }
                string FilePath = Path.Combine(FileConatiner, fileName);
                FileInfo file = new FileInfo(FilePath);
                file.Delete();
                return file;
        }


        public Person UpdatePhoto(AddPerson personWithFile)
        {
            if (personWithFile is null)
            {
                throw new ArgumentNullException(nameof(personWithFile));
            }
            if (personWithFile.Picture != null)
            {
                if (personWithFile.Person.FileName!=null)
                {
                string PictureFilePath = Path.Combine(FileConatiner,personWithFile.Person.FileName );
                using FileStream fileStream = new FileStream(PictureFilePath, FileMode.Create);
                personWithFile.Picture.CopyTo(fileStream);
                return personWithFile.Person;
                }
            }
            return AddPersonFile(personWithFile);
        }
    }
}