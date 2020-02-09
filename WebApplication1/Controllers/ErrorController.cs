using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApplication1.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        private readonly ILogger logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger;
        }

        [Route("Error")]

        public IActionResult Error()
        {

            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewBag.ErrorMessage ="An error occurred at the server";
            logger.LogError( $"An excepttion happened at {exceptionHandlerPathFeature.Path} " +
                $"with the following message : {exceptionHandlerPathFeature.Error.Message}");
            return View();
        }


        [Route("Error/{HttpStatusCode}")]
        public IActionResult ErrorCode(int HttpStatusCode)
        {
            ViewBag.ErrorMessage = HttpStatusCode switch
            {
                404 => "404 , Page not found",
                _ => $"Error code : {HttpStatusCode}",
            };
            return View(viewName: "Error");
        }

        [Route("PersonError/{id}")]
        public IActionResult PersonError(int? id)
        {
            if (id is null)
            {
                ViewBag.ErrorMessage = "incorrect action";
            }
            else
            {
                ViewBag.ErrorMessage = $"Person with id = {id} was not found";
            }
            return View(viewName:"Error");
        }

        [Route("Error/ThrowException")]
        public IActionResult ThrowException()
        {
            throw new Exception();
        }

    }
}
