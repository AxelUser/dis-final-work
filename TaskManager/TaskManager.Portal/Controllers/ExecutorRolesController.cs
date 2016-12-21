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
    public class ExecutorRolesController : Controller
    {
        private TaskManagerContext db = new TaskManagerContext();

        // GET: ExecutorRoles
        public ActionResult Index()
        {
            var executorRoles = db.ExecutorRoles.Include(e => e.RoleType).Include(e => e.Task).Include(e => e.User);
            return View(executorRoles.ToList());
        }

        // GET: ExecutorRoles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExecutorRole executorRole = db.ExecutorRoles.Find(id);
            if (executorRole == null)
            {
                return HttpNotFound();
            }
            return View(executorRole);
        }

        // GET: ExecutorRoles/Create
        public ActionResult Create()
        {
            ViewBag.ExecutorRoleTypeId = new SelectList(db.ExecutorRoleTypes, "Id", "Name");
            ViewBag.TaskId = new SelectList(db.ProjectTasks, "Id", "Title");
            ViewBag.UserId = new SelectList(db.Users, "Id", "PasswordHash");
            return View();
        }

        // POST: ExecutorRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,ExecutorRoleTypeId,TaskId")] ExecutorRole executorRole)
        {
            if (ModelState.IsValid)
            {
                db.ExecutorRoles.Add(executorRole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ExecutorRoleTypeId = new SelectList(db.ExecutorRoleTypes, "Id", "Name", executorRole.ExecutorRoleTypeId);
            ViewBag.TaskId = new SelectList(db.ProjectTasks, "Id", "Title", executorRole.TaskId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "PasswordHash", executorRole.UserId);
            return View(executorRole);
        }

        // GET: ExecutorRoles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExecutorRole executorRole = db.ExecutorRoles.Find(id);
            if (executorRole == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExecutorRoleTypeId = new SelectList(db.ExecutorRoleTypes, "Id", "Name", executorRole.ExecutorRoleTypeId);
            ViewBag.TaskId = new SelectList(db.ProjectTasks, "Id", "Title", executorRole.TaskId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "PasswordHash", executorRole.UserId);
            return View(executorRole);
        }

        // POST: ExecutorRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,ExecutorRoleTypeId,TaskId")] ExecutorRole executorRole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(executorRole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ExecutorRoleTypeId = new SelectList(db.ExecutorRoleTypes, "Id", "Name", executorRole.ExecutorRoleTypeId);
            ViewBag.TaskId = new SelectList(db.ProjectTasks, "Id", "Title", executorRole.TaskId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "PasswordHash", executorRole.UserId);
            return View(executorRole);
        }

        // GET: ExecutorRoles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExecutorRole executorRole = db.ExecutorRoles.Find(id);
            if (executorRole == null)
            {
                return HttpNotFound();
            }
            return View(executorRole);
        }

        // POST: ExecutorRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExecutorRole executorRole = db.ExecutorRoles.Find(id);
            db.ExecutorRoles.Remove(executorRole);
            db.SaveChanges();
            return RedirectToAction("Index");
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
