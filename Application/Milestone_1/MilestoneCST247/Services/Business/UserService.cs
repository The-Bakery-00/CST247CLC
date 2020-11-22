using MilestoneCST247.Models;
using MilestoneCST247.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MilestoneCST247.Services.Business
{
    public class UserService
    {


        public Boolean loggedIn(Controller c)
        {
            
            return c.Session["user"] != null;
        }

        public User loadUser(LoginRequest loginRequest)
        {
            UserDAO userDAO = new UserDAO();

            return userDAO.findUser(loginRequest);
        }



    }
}