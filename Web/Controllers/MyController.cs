using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class MyController : Controller
    {
        public MyEntities db = new MyEntities();    //khởi tạo biến load database lên
        // GET: My
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Indexa()
        {
            return View();
        }

        public ActionResult Course()
        {
            var course = from c in db.COURSEs
                        select c;
            var lesson = from l in db.LESSONs
                         select l;
            ViewBag.Course = course.ToList();
            ViewBag.Lesson = lesson.ToList();
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Blog()
        {
            return View();
        }
    }
}