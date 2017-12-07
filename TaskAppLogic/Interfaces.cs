using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskAppLogic
{
    public enum Priority
    {
        Low,
        Medium,
        High
    }

    public interface IUser
    {
        string UserName { get; }
        string Password { get; set; }
    }

    public interface ITask
    {
        string Title { get; set; }
        string Description { get; set; }
        IUser AssignedTo { get; set; }
        // XmlUser AssignedTo { get; set; }
        DateTime Due { get; set; }
        Priority Priority { get; set; }
    }

    public interface IUserDatabase
    {
        IUser GetUser(string username, string password);
        void SaveUser(IUser user);
        IUser Login(string username, string password);
    }

    public interface ITaskDatabase
    {
        IEnumerable<ITask> GetTasks(IUser user);
        ITask NewTask();
        void SaveTask(ITask task);
    }
}
