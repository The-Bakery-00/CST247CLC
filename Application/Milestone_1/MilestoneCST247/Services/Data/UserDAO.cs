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
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MineSweepr;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

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
                        Object o = new Object();

                        o = reader["ID"];
                        int id = new int();
                        // is value is not null assign to correctly and continue
                        if (o != null)
                        {
                            id = int.Parse(reader["ID"].ToString());
                        }
                        // else the program will exist and give post description in the consol
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("Null value in UserDAO/findUser() while pulling value from table users, colum ID");

                        }

                        o = reader["USERNAME"];
                        String userName = null;
                        // is value is not null assign to correctly and continue
                        if (o != null)
                        {
                            userName = reader["USERNAME"].ToString();
                        }
                        // else the program will exist and give post description in the consol
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("Null value in UserDAO/findUser() while pulling value from table users, colum USERNAME");

                        }

                        o = reader["PASSWORD"];
                        String password = null;
                        // is value is not null assign to correctly and continue
                        if (o != null)
                        {
                            password = reader["PASSWORD"].ToString();
                        }
                        // else the program will exist and give post description in the consol
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("Null value in UserDAO/findUser() while pulling value from table users, colum PASSWORD");

                        }

                        o = reader["FIRSTNAME"];
                        String firstName = null;
                        // is value is not null assign to correctly and continue
                        if (o != null)
                        {
                            firstName = reader["FIRSTNAME"].ToString();
                        }
                        // else the program will exist and give post description in the consol
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("Null value in UserDAO/findUser() while pulling value from table users, colum FIRSTNAME");

                        }

                        o = reader["LASTNAME"];
                        String lastName = null;
                        // is value is not null assign to correctly and continue
                        if (o != null)
                        {
                            lastName = reader["LASTNAME"].ToString();
                        }
                        // else the program will exist and give post description in the consol
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("Null value in UserDAO/findUser() while pulling value from table users, colum LASTNAME");

                        }

                        o = reader["EMAIL"];
                        String email = null;
                        // is value is not null assign to correctly and continue
                        if (o != null)
                        {
                            email = reader["EMAIL"].ToString();
                        }
                        // else the program will exist and give post description in the consol
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("Null value in UserDAO/findUser() while pulling value from table users, colum EMAIL");

                        }

                        o = reader["GENDER"];
                        String gender = null;
                        // is value is not null assign to correctly and continue
                        if (o != null)
                        {
                            gender = reader["GENDER"].ToString();
                        }
                        // else the program will exist and give post description in the consol
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("Null value in UserDAO/findUser() while pulling value from table users, colum GENDER");

                        }

                        o = reader["AGE"];
                        int age = new int();
                        // is value is not null assign to correctly and continue
                        if (o != null)
                        {
                            age = int.Parse(reader["AGE"].ToString());
                        }
                        // else the program will exist and give post description in the consol
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("Null value in UserDAO/findUser() while pulling value from table users, colum AGE");

                        }
                        
                        // create new user and assigned newly pulled values from DB to this user
                        user = new User(id, userName, password, firstName, lastName, email, gender, age);
                        
                        
                    }
                    // close connection to DB
                    connection.Close();
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            // return newly loaded user OR NULL user if no user exist
            return user;

        }

    }
}