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
    public class TaskStatusTypesController : Controller
    {
        private TaskManagerContext db = new TaskManagerContext();

        // GET: TaskStatusTypes
        public ActionResult Index()
        {
            return Json(db.TaskStatusTypes.ToList(), JsonRequestBehavior.AllowGet);
        }

        //// GET: TaskStatusTypes/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    TaskStatusType taskStatusType = db.TaskStatusTypes.Find(id);
        //    if (taskStatusType == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(taskStatusType);
        //}

        //// GET: TaskStatusTypes/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: TaskStatusTypes/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Caption")] TaskStatusType taskStatusType)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.TaskStatusTypes.Add(taskStatusType);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(taskStatusType);
        //}

        //// GET: TaskStatusTypes/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    TaskStatusType taskStatusType = db.TaskStatusTypes.Find(id);
        //    if (taskStatusType == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(taskStatusType);
        //}

        //// POST: TaskStatusTypes/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Caption")] TaskStatusType taskStatusType)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(taskStatusType).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(taskStatusType);
        //}

        //// GET: TaskStatusTypes/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    TaskStatusType taskStatusType = db.TaskStatusTypes.Find(id);
        //    if (taskStatusType == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(taskStatusType);
        //}

        //// POST: TaskStatusTypes/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    TaskStatusType taskStatusType = db.TaskStatusTypes.Find(id);
        //    db.TaskStatusTypes.Remove(taskStatusType);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
