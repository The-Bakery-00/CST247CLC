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
        //SecurityDAO daoService = new SecurityDAO();

        public LoginResponse Authenticate(LoginRequest loginRequest)
        {
            LoginResponse response = new LoginResponse();
            response.Success = false;

            SecurityDAO dataService = new SecurityDAO();

            if (dataService.validUser(loginRequest))
            {
                response.Success = true;
            }
            else
            {
                response.Message = "Invalid username or password.";
            }

            return response;
        }
    }
}