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
using StudentDataAPI.Data;
using StudentDataAPI.Models;

namespace StudentDataAPI.Controllers
{
    public class StudentDataController : ApiController
    {
        private StudentDataAPIContext db = new StudentDataAPIContext();

        // GET: api/StudentData       
        public List<StudentData> GetStudentDatas()
        {
            return db.StudentDatas.ToList();
        }

        // GET: api/StudentData/5
        [ResponseType(typeof(StudentData))]
        public IHttpActionResult GetStudentData(int id)
        {
            StudentData studentData = db.StudentDatas.Find(id);
            if (studentData == null)
            {
                return NotFound();
            }

            return Ok(studentData);
        }

        // POST: api/StudentData
        [ResponseType(typeof(StudentData))]
        public IHttpActionResult PostStudentData(StudentData studentData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StudentDatas.Add(studentData);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = studentData.ID }, studentData);
        }

        // DELETE: api/StudentData/5
        [ResponseType(typeof(StudentData))]
        public IHttpActionResult DeleteStudentData(int id)
        {
            StudentData studentData = db.StudentDatas.Find(id);
            if (studentData == null)
            {
                return NotFound();
            }

            db.StudentDatas.Remove(studentData);
            db.SaveChanges();

            return Ok(studentData);
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