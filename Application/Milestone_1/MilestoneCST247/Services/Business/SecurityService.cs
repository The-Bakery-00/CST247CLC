using MilestoneCST247.Models;
using MilestoneCST247.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MilestoneCST247.Services.Business
{
    public class SecurityService
    {
        SecurityDAO daoService = new SecurityDAO();

        public bool Authenticate(UserModel user)
        {
            return daoService.FindByUser(user);
        }
    }
}