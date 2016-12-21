using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.DAL.Entities;
using TaskManager.Utils;
using System.Data.Entity;

namespace TaskManager.Service.Notification.Handler
{
    public class NotificationHandler
    {
        List<Task> handlerTasks;
        EmailSender emailSender;
        bool stopped;       

        public NotificationHandler(EmailSender emailSender)
        {
            this.emailSender = emailSender;
            handlerTasks = new List<Task>();
            stopped = false;
        }

        public void RunSending(int taskId)
        {
            var newTask = new Task(() =>
            {
                var title = GetTaskTitle(taskId);
                if (!string.IsNullOrEmpty(title))
                {
                    var emails = GetEmailsForTask(taskId);
                    SendEmails(title, emails);
                }
            });
            lock (handlerTasks)
            {
                handlerTasks.Add(newTask);
            }
            newTask.ContinueWith(t =>
            {
                lock (handlerTasks)
                {
                    handlerTasks.Remove(t);
                }
            });
            if (!stopped)
            {
                newTask.Start();
            }            
        }

        public void StopSendingTasks()
        {
            Task.WaitAll(handlerTasks.ToArray());
        }

        private void SendEmails(string taskTitle, List<string> adresses)
        {
            Parallel.ForEach(adresses, adr => SendEmail(taskTitle, adr));
        }

        private bool SendEmail(string taskTitle, string adress)
        {
            string title = $"Задача \"{taskTitle}\" была изменена.";
            string body = $"<h2>{title}<h2>";

            return emailSender.SendAsync(adress, title, body).GetAwaiter().GetResult();
        }

        private List<string> GetEmailsForTask(int taskId)
        {
            using(TaskManagerContext db = new TaskManagerContext())
            {
                var emails = db.ExecutorRoles.Include(r=>r.User)
                        .Where(r => r.TaskId == taskId)
                        .Select(r => r.User.Email)
                        .ToList();
                return emails;
            }
        }

        private string GetTaskTitle(int taskId)
        {
            using(TaskManagerContext db = new TaskManagerContext())
            {
                string taskName = db.ProjectTasks.Find(taskId)?.Title;
                return taskName;
            }
        }
    }
}
