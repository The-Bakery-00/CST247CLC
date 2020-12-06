using MilestoneCST247.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MilestoneCST247.Services.Data
{
    public class RegSecurityDAO
    {
        // way to connect to the DB
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MineSweepr;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public bool userExists(RegisterRequest registerRequest)
        {
            bool success = false;

            

            string queryString = "SELECT * FROM dbo.Users WHERE username = @UserName";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // create the command and parameter objects

                SqlCommand command = new SqlCommand(queryString, connection);

                // assosiates @UserName and @Password with user.UserNmae and User.Passwords
                command.Parameters.Add("@UserName", System.Data.SqlDbType.VarChar, 50).Value = registerRequest.Username;


                // open the database and run the command
                try
                {

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        success = true;
                    }
                    else
                    {
                        success = false;
                    }
                    connection.Close();

                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("Catch Exception in RegSecurityDAO method userExist() + exception message = " + e);

                }
                return success;
            }
        }

        public bool emailExists(RegisterRequest registerRequest)
        {
            bool success = false;



            string queryString = "SELECT * FROM dbo.Users WHERE email = @Email";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // create the command and parameter objects

                SqlCommand command = new SqlCommand(queryString, connection);

                // assosiates @UserName and @Password with user.UserNmae and User.Passwords
                command.Parameters.Add("@Email", System.Data.SqlDbType.VarChar, 50).Value = registerRequest.Email;


                // open the database and run the command
                try
                {

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        success = true;
                    }
                    else
                    {
                        success = false;
                    }
                    connection.Close();

                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("Catch Exception in userExist() method emailExist() + exception message = " + e);
                }
                return success;
            }
        }

        public bool createUser(RegisterRequest registerRequest)
        {
            bool success = false;



            string queryString = "INSERT INTO dbo.Users (username, password, email, firstname, lastname, age, gender) VALUES (@UserName, @Password, @Email, @FirstName, @LastName, @Age, @Gender)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // create the command and parameter objects

                SqlCommand command = new SqlCommand(queryString, connection);

                // assosiates @UserName and @Password with user.UserNmae and User.Passwords
                command.Parameters.Add("@UserName", System.Data.SqlDbType.VarChar, 50).Value = registerRequest.Username;
                command.Parameters.Add("@Password", System.Data.SqlDbType.VarChar, 50).Value = registerRequest.Password;
                command.Parameters.Add("@Email", System.Data.SqlDbType.VarChar, 50).Value = registerRequest.Email;
                command.Parameters.Add("@FirstName", System.Data.SqlDbType.VarChar, 50).Value = registerRequest.FirstName;
                command.Parameters.Add("@LastName", System.Data.SqlDbType.VarChar, 50).Value = registerRequest.LastName;
                command.Parameters.Add("@Age", System.Data.SqlDbType.Int, 11).Value = registerRequest.Age;
                command.Parameters.Add("@Gender", System.Data.SqlDbType.VarChar, 50).Value = registerRequest.Gender;


                // open the database and run the command
                try
                {

                    connection.Open();
                    //SqlDataReader reader = command.ExecuteReader();
                    int rows = command.ExecuteNonQuery();
                    if (rows == 1)
                        success = true;
                    else
                        success = false;
                    connection.Close();

                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("Catch Exception in RegSecurityDAO method createUser() + exception message = " + e);
                }
                return success;
            }
        }

    }
}