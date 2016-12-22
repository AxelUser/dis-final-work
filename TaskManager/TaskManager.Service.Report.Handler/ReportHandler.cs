using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.DAL.Entities;
using TaskManager.Utils;
using System.Data.Entity;
using TaskManager.Portal.BLL.DTO;
using TaskManager.Utils.Reports;
using TaskManager.Portal.BLL.Extentions;

namespace TaskManager.Service.Notification.Handler
{
    public class ReportHandler
    {
        List<Task> handlerTasks;
        EmailSender emailSender;
        bool stopped;       

        public ReportHandler(EmailSender emailSender)
        {
            this.emailSender = emailSender;
            handlerTasks = new List<Task>();
            stopped = false;
        }

        public void RunSending(int projectId)
        {
            var newTask = new Task(() =>
            {
                string title;
                List<ProjectTaskReportViewModel> models = new List<ProjectTaskReportViewModel>();
                using (TaskManagerContext db = new TaskManagerContext())
                {
                    var project = db.Projects
                        .SingleOrDefault(p=>p.Id==projectId);
                    var tasks = db.ProjectTasks
                        .Where(t => t.ProjectId == projectId).ToList();
                    title = project?.Title;
                    if(tasks != null)
                    {
                        models = tasks.Select(t => t.ToReportViewModel()).ToList();
                    }
                }
                if (!string.IsNullOrEmpty(title))
                {
                    var email = GetEmailForProject(projectId);
                    var file = CreateReport(title, models);
                    SendReport(title, email, file);
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

        private bool SendReport(string projectTitle, string adress, byte[] file)
        {
            string title = $"Отчет по проекту \"{projectTitle}\".";
            string body = $"<h2>{title}<h2>";
            return emailSender.SendFile(adress, title, body, file, "project_report.xlsx");
        }

        private string GetEmailForProject(int projectId)
        {
            using(TaskManagerContext db = new TaskManagerContext())
            {
                var email = db.Projects.Include(p => p.User)
                    .SingleOrDefault(p => p.Id == projectId)?.User.Email;
                return email;
            }
        }

        private string GetProjectTitle(int taskId)
        {
            using(TaskManagerContext db = new TaskManagerContext())
            {
                string taskName = db.Projects.Find(taskId)?.Title;
                return taskName;
            }
        }

        private byte[] CreateReport(string title, List<ProjectTaskReportViewModel> tasks)
        {
            byte[] file = ExcelReporterFactory.CreateCustomReport<ProjectTaskReportViewModel>(title)
                .SetDefHeaderStyle(new CellStyle()
                {
                    Border = true,
                    CellsColor = System.Drawing.Color.FromArgb(155, 194, 230)
                })
                .SetDefDataCellStyle(new CellStyle()
                {
                    Border = true
                })
                .AddColumn(vm => new CustomColumn()
                {
                    Name = "Название",
                    Value = vm.Title,
                    Width = 40
                })
                .AddColumn(vm => new CustomColumn()
                {
                    Name = "Оценка сложности",
                    Value = vm.EstimatedDifficult,
                    Width = 15
                })
                .AddColumn(vm => new CustomColumn()
                {
                    Name = "Обновлен",
                    Value = vm.UpdateDate.ToShortDateString(),
                    Width = 20
                })
                .AddColumn(vm => new CustomColumn()
                {
                    Name = "Создан",
                    Value = vm.CreationDate.ToShortDateString(),
                    Width = 20
                })
                .Create(tasks);
            return file;
        }
    }
}
