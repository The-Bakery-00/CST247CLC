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

        public RegisterResponse Authenticate(RegisterRequest registerRequest)
        {

            RegisterResponse response = new RegisterResponse();
            response.Success = false;

            RegSecurityDAO dataService = new RegSecurityDAO();



            if (dataService.userExists(registerRequest))
            {
                response.Message = "Username already exists.";
            }
            else if (dataService.emailExists(registerRequest))
            {
                response.Message = "Email already in use.";
            }
            else if (dataService.createUser(registerRequest))
            {
                response.Success = true;
            }


            return response;

        }

    }
}