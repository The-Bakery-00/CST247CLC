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
    public class LoginController : Controller
    {
        private readonly ILogger logger;

        public LoginController(ILogger logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public ActionResult Index()
        {
            logger.Info("Entered into the Login Controller.");
            return View("Login");
        }

        [HttpPost]
        public ActionResult doLogin(LoginRequest loginRequest)
        {
            logger.Info("doLogin method has been executed.");
            LoginResponse response;

            if (ModelState.IsValid)
            {
                logger.Info("doLogin ModelState is valid.");
                SecurityService ss = new SecurityService();
                response = ss.Authenticate(loginRequest);

                if (response.Success)
                {
                    logger.Info("doLogin has returned a successful response.");
                    //load user model and throw in session var
                    UserService userService = new UserService();
                    var user = userService.loadUser(loginRequest);
                    Session["user"] = user;

                    return View("LoginSuccess", loginRequest);

                }
            }
            else
            {
                logger.Info("There was an issue validating the model in the doLogin method.");
                string errors = string.Join("<br/> ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                response = new LoginResponse(false, errors);
            }
            logger.Info("doLogin Failed to parse login credentials.");
            return View("LoginFailure", response);

        }
    }
}