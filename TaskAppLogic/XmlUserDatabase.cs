using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace TaskAppLogic
{
    public class XmlUserDatabase : IUserDatabase
    {
        public XmlUserDatabase(string filename)
        {
            mFilename = filename;
            if (File.Exists(filename))
            {
                using (FileStream input = File.OpenRead(mFilename))
                {
                    validUsers = (List<XmlUser>)mSerializer.Deserialize(input);
                }
            }
        }

        public IUser GetUser(string username, string password)
        {
            var user = new XmlUser();
            user.UserName = username;
            user.Password = password;
            return user;
        }


        public void SaveUser(IUser user)
        {
            if (!(user is XmlUser))
            {
                throw new ArgumentException("I can only handle users I created.");
            }

            var xmlUser = (XmlUser)user;

            if (!validUsers.Contains(xmlUser))
            {
                validUsers.Add(xmlUser);
            }

            using (FileStream output = File.Create(mFilename))
            {
                mSerializer.Serialize(output, validUsers);
            }
        }
        

        public IUser Login(string username, string password)
        {
            foreach (XmlUser user in validUsers)
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

        private readonly List<XmlUser> validUsers = new List<XmlUser>(); // do we need readonly?
        private readonly string mFilename;
        private readonly XmlSerializer mSerializer = new XmlSerializer(typeof(List<XmlUser>));
        

    }


    public class XmlUser : IUser
    {
//        public string UserName { get; internal set; } // caused runtime error because there was no public setter for UserName. 
        public string UserName { get; set; }
        public string Password { get; set; }
    }



}
