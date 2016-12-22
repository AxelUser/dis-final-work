using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskManager.DAL.Entities;

namespace TaskManager.Portal.Controllers
{
    public class ProjectTasksController : Controller
    {
        private TaskManagerContext db = new TaskManagerContext();

        // GET: ProjectTasks
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var project = db.Projects.Include(p => p.ProjectTasks).Where(p => p.Id == id).Single();
            return Json(project.ProjectTasks.ToList(), JsonRequestBehavior.AllowGet);
        }

        // GET: ProjectTasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectTask projectTask = db.ProjectTasks.Find(id);
            if (projectTask == null)
            {
                return HttpNotFound();
            }
            return Json(projectTask, JsonRequestBehavior.AllowGet);
        }

        // POST: ProjectTasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(ProjectTask projectTask)
        {
            if (ModelState.IsValid)
            {
                var type = projectTask.TaskStatusType;
                if (type != null)
                {
                    projectTask.TaskStatusTypeId = projectTask.TaskStatusType != null ? projectTask.TaskStatusType.Id : projectTask.TaskStatusTypeId;
                    projectTask.TaskStatusType = null;
                }
                projectTask.CreationDate = DateTime.Now;
                projectTask.UpdateDate = DateTime.Now;
                db.ProjectTasks.Add(projectTask);
                db.SaveChanges();
                projectTask.TaskStatusType = type;
                return Json(projectTask, JsonRequestBehavior.AllowGet);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // POST: ProjectTasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(ProjectTask projectTask)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projectTask).State = EntityState.Modified;
                db.SaveChanges();
                return Json(projectTask, JsonRequestBehavior.AllowGet);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // POST: ProjectTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ProjectTask projectTask = db.ProjectTasks.Find(id);
            db.ProjectTasks.Remove(projectTask);
            db.SaveChanges();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
