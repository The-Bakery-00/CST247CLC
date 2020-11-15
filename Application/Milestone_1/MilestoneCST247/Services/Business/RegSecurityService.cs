using MilestoneCST247.Models;
using MilestoneCST247.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MilestoneCST247.Services.Business
{
    public class RegSecurityService
    {

        RegSecurityDAO daoService = new RegSecurityDAO();

        public bool Validate(UserModel user)
        {
            if(daoService.userExists(user))
            {
                return false;
            }
            else if(daoService.emailExists(user))
            {
                return false;
            }
            else if (daoService.createUser(user))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}