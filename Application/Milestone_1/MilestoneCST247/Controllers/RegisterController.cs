using MilestoneCST247.Models;
using MilestoneCST247.Services.Business;
using MilestoneCST247.Services.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MilestoneCST247.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ILogger logger;

        public RegisterController(ILogger logger)
        {
            this.logger = logger;
        }
        [HttpGet]
        public ActionResult Index()
        {
            logger.Info("Entered into the RegisterController");
            return View("Register");
        }

        [HttpGet]
        public ActionResult Login()
        {
            logger.Info("Entering into Login from RegisterController");
            return View("Login");
        }



        [HttpPost]
        public ActionResult Register(RegisterRequest registerRequest)
        {
            RegSecurityService ss = new RegSecurityService();
            RegisterResponse response;

            if (ModelState.IsValid)
            {
                response = ss.Authenticate(registerRequest);

                if (response.Success)
                {
                    logger.Info("Successfully registered user with Register()");
                    return View("RegistrationSuccess", registerRequest);

                }
            }
            else
            {
                string errors = string.Join("<br/> ", ModelState.Values
                        .SelectMany(x => x.Errors)
                        .Select(x => x.ErrorMessage));
                response = new RegisterResponse(false, errors);
            }
            logger.Info("Failed to register user using the Regsiter()");
            return View("RegistrationFailure", response);

        }

    }
}