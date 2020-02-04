using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Module.PersonRepositories;
using WebApplication1.Module;
using WebApplication1.ViewModule;
using WebApplication1.Module.FileRepositories;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPersonRepository personRepository;
        private readonly IFileRepository fileRepository;

        public HomeController(IPersonRepository personRepository, IFileRepository fileRepository )
        {
            this.personRepository = personRepository;
            this.fileRepository = fileRepository;
        }

        public IActionResult Index()
        {

            var People = personRepository.GetPeopleBasicInfo();
            
            return View(People);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddPerson personWithFile)
        {
            if (ModelState.IsValid)
            {
                Person person = fileRepository.AddPersonFile(personWithFile);
                personRepository.AddPerson(person);
                return RedirectToAction("Index");
            }
            return View(personWithFile);
        }

    }
}
