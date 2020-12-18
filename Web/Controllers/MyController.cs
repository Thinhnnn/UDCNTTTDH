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
                         where c.Status == true
                        select c;
            var lesson = from l in db.LESSONs
                         where l.Status == true
                         select l;
            ViewBag.Course = course.ToList();
            ViewBag.Lesson = lesson.ToList();
            return View();
        }

        public ActionResult MyCourse()
        {
            if (Session["UserName"] != null)
            {
                int UserID = int.Parse(Session["UserID"].ToString());
                var course = from c in db.COURSEs
                             where c.Status == true
                             select c;
                var myCourse = from m in db.REGISTERs
                               where m.IdUser == UserID && m.Status == true
                               select m.IdCourse;
                ViewBag.RegisteredCourseID = myCourse.ToList();
                ViewBag.Course = course.ToList();
                return View();
            }
            else
            {
                return View("Index");
            }
        }

        public ActionResult CourseDetail(int id)
        {
            var courseName = from c in db.COURSEs
                         where c.Status == true && c.Id == id
                         select c.NameOfTheCourse;
            var lesson = from l in db.LESSONs
                         where l.Status == true && l.IdCourse == id
                         orderby l.IndexNumber descending
                         select l;
            ViewBag.Course = courseName.ToString();
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