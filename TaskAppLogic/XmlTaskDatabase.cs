﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TaskAppLogic
{
    public class XmlTaskDatabase : ITaskDatabase // wouldn't work unless public for program.cs
    {
        public XmlTaskDatabase(string filename)
        {
            if (File.Exists(filename))
            {
                mFilename = filename;
                using (FileStream input = File.OpenRead(mFilename))
                {
                    mTasks = (List<XmlTask>)mSerializer.Deserialize(input);
                }
            }
        }

        public IEnumerable<ITask> GetTasks(IUser user)
        {
            foreach (XmlTask task in mTasks)
            {
                if (task.AssignedTo == user)
                {
                    yield return task;
                }
            }
        }

        public ITask NewTask()
        {
            return new XmlTask();
        }

        public void SaveTask(ITask task)
        {
            if (!(task is XmlTask))
            {
                throw new ArgumentException("I can only handle tasks I created.");
            }

            var xmlTask = (XmlTask)task;

            if (!mTasks.Contains(xmlTask))
            {
                mTasks.Add(xmlTask);
            }

            using (FileStream output = File.Create(mFilename))
            {
                mSerializer.Serialize(output, mTasks);
            }
        }

        private readonly string mFilename;
        private List<XmlTask> mTasks = new List<XmlTask>();
        private readonly XmlSerializer mSerializer = new XmlSerializer(typeof(List<XmlTask>));
    }

    public class XmlTask : ITask
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IUser AssignedTo { get; set; }
        public DateTime Due { get; set; }
        public Priority Priority { get; set; }
    }
}
