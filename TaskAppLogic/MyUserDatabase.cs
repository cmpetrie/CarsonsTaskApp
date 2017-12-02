using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskAppLogic
{
    public class MyUserDatabase : IUserDatabase
    {
        public IUser GetUser(string username, string password)
        {
            var user = new MyUser();
            user.UserName = username;
            user.Password = password;
            return user;
        }

        public void SaveUser(IUser user)
        {
            if (user is MyUser)
            {
                validUsers.Add((MyUser)user);
            }
            else
            {
                throw new ArgumentException("I want a User!");
            }
        }

        public IUser Login(string username, string password)
        {
            foreach (MyUser user in validUsers)
            {
                if (user.UserName.Equals(username))
                {
                    if (user.Password.Equals(password))
                    {
                        return user;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            return null;
        }

        private readonly List<MyUser> validUsers = new List<MyUser>();
    }


    public class MyUser : IUser
    {
        public string UserName { get; internal set; }
        public string Password { get; set; }
    }



}
