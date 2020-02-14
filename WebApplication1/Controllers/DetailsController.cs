using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Module;
using WebApplication1.Module.FileRepositories;
using WebApplication1.Module.PersonRepositories;
using WebApplication1.ViewModule;

namespace WebApplication1.Controllers
{
    public class DetailsController : Controller
    {
        private readonly IPersonRepository personRepository;
        private readonly IFileRepository fileRepository;

        public DetailsController(IPersonRepository personRepository, IFileRepository fileRepository)
        {
            this.personRepository = personRepository;
            this.fileRepository = fileRepository;
        }

        public IActionResult Detail(int? id)
        {
            id ??= personRepository.GetPeopleBasicInfo().Min(p => p.ID);
            PersonDetails personDetails = new PersonDetails
            {
                Person = personRepository.GetPerson(id)
            };
            if (personDetails.Person is null)
            {
                return RedirectToAction(nameof(ErrorController.PersonError),nameof(ErrorController)[0..^10], new { id });
            }
            personDetails.Person = fileRepository.CheckFile(personDetails.Person);
            return View(personDetails);
        }

        public RedirectToActionResult Delete(int id)
        {
            Person ToDelete = personRepository.GetPerson(id);
            if (ToDelete is null)
            {
                return RedirectToAction(nameof(ErrorController.PersonError), nameof(ErrorController)[0..^10], new { id });
            }
            personRepository.DeletePerson(ToDelete);
            fileRepository.RemoveFile(ToDelete.FileName);
            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController)[0..^10]);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Person person = personRepository.GetPerson(id);
            if (person is null)
            {
                return RedirectToAction(nameof(ErrorController.PersonError), nameof(ErrorController)[0..^10], new { id });
            }
                return View(person);
        }

        [HttpPost]
        public IActionResult Update(Person person)
        {
            if (ModelState.IsValid)
            {
                personRepository.UpdatePerson(person);
                return RedirectToAction(actionName: nameof(Detail), routeValues: new { id = person.ID });
            }
            return View(person);
        }

        [HttpGet]
        public IActionResult UpdatePhoto(int id)
        {
            Person person = personRepository.GetPerson(id);
            if (person is null)
            {
                return RedirectToAction(nameof(ErrorController.PersonError), nameof(ErrorController)[0..^10], new { id });
            }
            UploadPicture uploadPicture = new UploadPicture
            {
                FileName = person.FileName,
                PersonID = person.ID,

            };
            return View(uploadPicture);
        }

        [HttpPost]
        public IActionResult UpdatePhoto(UploadPicture uploadPicture)
        {
            if (!ModelState.IsValid)
            {
                return View(uploadPicture);
            }
            AddPerson personWithFile = new AddPerson
            {
                Person = personRepository.GetPerson(uploadPicture.PersonID),
                Picture = uploadPicture.FormFile
            };
            if (personWithFile.Person is null)
            {
                return RedirectToAction(nameof(ErrorController.PersonError),
                    nameof(ErrorController)[0..^10], new { id = uploadPicture.PersonID });
            }
            Person person = fileRepository.UpdatePhoto(personWithFile);
            personRepository.UpdatePerson(person);
            return RedirectToAction(actionName: nameof(Detail), routeValues: new { id = personWithFile.Person.ID });
        }
    }
}
