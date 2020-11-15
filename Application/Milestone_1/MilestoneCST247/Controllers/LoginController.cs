﻿using MilestoneCST247.Models;
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
        // GET: Login
        public ActionResult Index()
        {
            return View("Login");
        }

        public ActionResult Login(UserModel userModel)
        {
            //return "Result: Username = " + userModel.UserName + " PW = " + userModel.Password;

            SecurityService securityService = new SecurityService();
            Boolean success = securityService.Authenticate(userModel);

            if (success)
            {
                return View("LoginSuccess", userModel);
            }
            else
            {
                return View("LoginFailure");
            }
        }

    }
}