using MilestoneCST247.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MilestoneCST247.Services.Data
{
    public class SecurityDAO
    {

        // way to connect to the DB
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MineSweepr;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        internal Boolean validUser(LoginRequest loginRequest)
        {
            // start by assuming login query failed
            bool success = false;

            // write an sql expression
            string queryString = "SELECT * FROM dbo.Users WHERE username = @UserName AND password = @Password";

            // create and open the connection to the DB inside the block
            // this makes it so that all resources are closed properly when the query is done

            using(SqlConnection connection = new SqlConnection(connectionString) )
            {
                // create the command and parameter objects

                SqlCommand command = new SqlCommand(queryString, connection);

                // assosiates @UserName and @Password with user.UserNmae and User.Passwords
                
                command.Parameters.Add("@UserName", System.Data.SqlDbType.VarChar, 50).Value = loginRequest.Username;
                command.Parameters.Add("@Password", System.Data.SqlDbType.VarChar, 50).Value = loginRequest.Password;

                // open the database and run the command
                try
                {

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        success = true;
                    } else
                    {
                        success = false;
                    }
                    connection.Close();

                }catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return success;
            }

        }
    }
}