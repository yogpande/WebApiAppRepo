using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApiApp.Models;

namespace WebApiApp.Controllers
{
    public class TaskController : ApiController
    {
        private DBEntities db = new DBEntities();

        // GET: api/Task
        public IQueryable<myTask> GetmyTasks()
        {
            return db.myTasks;
        }

        // GET: api/Task/5
        [ResponseType(typeof(myTask))]
        public IHttpActionResult GetmyTask(int id)
        {
            myTask myTask = db.myTasks.Find(id);
            if (myTask == null)
            {
                return NotFound();
            }

            return Ok(myTask);
        }

        // PUT: api/Task/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutmyTask(int id, myTask myTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != myTask.taskid)
            {
                return BadRequest();
            }

            db.Entry(myTask).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!myTaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Task
        [ResponseType(typeof(myTask))]
        public IHttpActionResult PostmyTask(myTask myTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.myTasks.Add(myTask);
            db.SaveChanges();

            return Ok("Success");
        }

        // DELETE: api/Task/5
        [ResponseType(typeof(myTask))]
        public IHttpActionResult DeletemyTask(int id)
        {
            myTask myTask = db.myTasks.Find(id);
            if (myTask == null)
            {
                return NotFound();
            }

            db.myTasks.Remove(myTask);
            db.SaveChanges();

            return Ok(myTask);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool myTaskExists(int id)
        {
            return db.myTasks.Count(e => e.taskid == id) > 0;
        }
    }
}