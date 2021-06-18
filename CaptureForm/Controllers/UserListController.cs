using CaptureForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static CaptureForm.Models.XMLHelper;

namespace CaptureForm.Controllers
{
    public class UserListController : Controller
    {
        XMLHelper XmlHelper = new XMLHelper();
        // GET: UserList
        public ActionResult Index()
        {
            var data = XmlHelper.ReturnListOfUsers();
            return View(data.ToList());
        }

        public JsonResult GetById(int id)
        {
            var user = XmlHelper.GetUserById(id);
            return Json(user, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int Id)
        {
            XmlHelper.DeleteUser(Id);
            return RedirectToAction("Index", "UserList");
        }

        [HttpPost]
        public ActionResult AddEditProject(Users user)
        {
            XmlHelper.AddEditUser(user);
            return RedirectToAction("Index", "UserList");
        }
    }
}