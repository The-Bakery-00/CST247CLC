using MilestoneCST247.Models;
using MilestoneCST247.Services.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MilestoneCST247.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View("Login");
        }


        [HttpPost]
        public ActionResult doLogin(LoginRequest loginRequest)
        {

            LoginResponse response;

            if (ModelState.IsValid)
            {

                SecurityService ss = new SecurityService();
                response = ss.Authenticate(loginRequest);

                if (response.Success)
                {

                    //load user model and throw in session var
                    UserService userService = new UserService();
                    var user = userService.loadUser(loginRequest);
                    this.Session["user"] = user;
                    


                    return View("LoginSuccess", loginRequest);

                }
            }
            else
            {
                string errors = string.Join("<br/> ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                response = new LoginResponse(false, errors);
            }
            return View("LoginFailure", response);


        }

    }
}