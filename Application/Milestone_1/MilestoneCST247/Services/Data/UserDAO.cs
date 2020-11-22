using MilestoneCST247.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MilestoneCST247.Services.Data
{
    public class UserDAO
    {



        internal User findUser(LoginRequest loginRequest)
        {
            User user = null;

            // way to connect to the DB
            string connectionString = @"Data Source=(localdb)\ProjectsV13;Initial Catalog=Users;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            // write an sql expression
            string queryString = "SELECT * FROM dbo.users WHERE username = @UserName AND password = @Password";

            // create and open the connection to the DB inside the block
            // this makes it so that all resources are closed properly when the query is done
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                    // create the command and parameter objects

                    SqlCommand command = new SqlCommand(queryString, connection);

                    // assosiates @UserName and @Password with user.UserNmae and User.Passwords
                    command.Parameters.Add("@UserName", System.Data.SqlDbType.VarChar, 50).Value = loginRequest.Username;
                    command.Parameters.Add("@Password", System.Data.SqlDbType.VarChar, 50).Value = loginRequest.Password;

                    // open the database and run the command
                
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int id = int.Parse(reader["ID"].ToString());
                        String userName = reader["USERNAME"].ToString();
                        String password = reader["PASSWORD"].ToString();
                        String email = reader["EMAIL"].ToString();
                        String firstName = reader["FIRSTNAME"].ToString();
                        String lastName = reader["LASTNAME"].ToString();
                        String gender = reader["SEX"].ToString();
                        int age = int.Parse(reader["AGE"].ToString());
                        

                        user = new User(id, userName, password, firstName, lastName, email, gender, age);
                        
                        
                    }
                    connection.Close();
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return user;

        }

    }
}