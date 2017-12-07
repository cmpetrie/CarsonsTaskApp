using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAppLogic;


namespace TaskApp
{
    class Program
    {
        static void Main(string[] args)
        {

            IUserDatabase userDb = new XmlUserDatabase("XmlUsers.xml"); // create a new user database
            IUser carson = userDb.GetUser("Carson", "cpword"); //random user
            userDb.SaveUser(carson); // add this user to the list. 

            IUser daddy = userDb.GetUser("daddy", "daddyiscool");
            userDb.SaveUser(daddy); // add this user to the list. 

            ITaskDatabase taskDb = new XmlTaskDatabase("XmlTasks.xml"); // create a new task database

            ITask task1 = taskDb.NewTask();
            task1.AssignedTo = carson;
            task1.Title = "scrub the apple";
            taskDb.SaveTask(task1);

            ITask task2 = taskDb.NewTask();
            task2.AssignedTo = carson;
            task2.Title = "wash the banana";
            taskDb.SaveTask(task2);

            ITask task3 = taskDb.NewTask();
            task3.AssignedTo = daddy;
            task3.Title = "wipe the honeydew";
            taskDb.SaveTask(task3);

            ITask task = taskDb.NewTask();
            task.AssignedTo = daddy;
            task.Title = "shine the watermelon";
            taskDb.SaveTask(task);

            Console.WriteLine("Enter your username!");
            string username = Console.ReadLine();
            Console.WriteLine("Enter your password");
            string password = Console.ReadLine();
            IUser loggedUser = userDb.Login(username , password);

            if (loggedUser != null)
            {
                Console.WriteLine("Do you want to see your tasks?");
                if (Console.ReadLine()[0] == 'y')
                {
                    var userTasks = taskDb.GetTasks(loggedUser); // What do I use instead of var?
                    foreach (ITask oneTask in userTasks)
                    {
                        Console.WriteLine(oneTask.Title);
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid user login. Please try again");
            }

        }
    }
}
