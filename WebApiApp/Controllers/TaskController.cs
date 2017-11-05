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
   
      
     public class UserAPIController : BaseAPIController
    {
        private DBEntities db = new DBEntities();

        public HttpResponseMessage Get()
        {
            return ToJson(UserDB.udatas.AsEnumerable());
        }

        public HttpResponseMessage Post([FromBody]udata value)
        {
            UserDB.udatas.Add(value);
            return ToJson(UserDB.SaveChanges());
        }

        public HttpResponseMessage Put(int id, [FromBody]udata value)
        {
            UserDB.Entry(value).State = EntityState.Modified;
            return ToJson(UserDB.SaveChanges());
        }
        public HttpResponseMessage Delete(int id)
        {
            UserDB.udatas.Remove(UserDB.udatas.FirstOrDefault(x => x.uid == id));
            return ToJson(UserDB.SaveChanges());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool udataExists(int id)
        {
            return db.udatas.Count(e => e.uid == id) > 0;
        }
    }
}
