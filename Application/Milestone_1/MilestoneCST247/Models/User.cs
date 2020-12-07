using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MilestoneCST247.Models
{
    [Serializable]
    public class User
    {
        private int id;
        private string username;
        private string password;
        private string email;
        private string firstName;
        private string lastName;
        private string gender;
        private int age;


        public User()
        {
            this.id = -1;
            this.username = "";
            this.password = "";
            this.email = "";
            this.firstName = "";
            this.lastName = "";
            this.gender = "";
            this.age = 0;


        }

        public User(int id, string username, string password, string email, string firstName, string lastName, string gender, int age)
        {
            this.id = id;
            this.username = username;
            this.password = password;
            this.email = email;
            this.firstName = firstName;
            this.lastName = lastName;
            this.gender = gender;
            this.age = age;

        }

        public int Id { get => id; set => id = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string Email { get => email; set => email = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Gender { get => gender; set => gender = value; }
        public int Age { get => age; set => age = value; }
    }
}