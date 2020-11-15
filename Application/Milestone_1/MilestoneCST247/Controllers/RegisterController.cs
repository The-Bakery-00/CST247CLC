using MilestoneCST247.Models;
using MilestoneCST247.Services.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MilestoneCST247.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return View("Register");
        }

        public ActionResult Register(UserModel userModel)
        {
            RegSecurityService securityService = new RegSecurityService();
            Boolean success = securityService.Validate(userModel);

            if (success)
            {
                return View("RegistrationSuccess", userModel);
            }
            else
            {
                return View("RegistrationFailure");
            }
        }

    }
}