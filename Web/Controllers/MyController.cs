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
            ViewBag.Lesson = lesson.ToList().Take(4);
            if (Session["UserName"] != null)
            {
                int UserID = int.Parse(Session["UserID"].ToString());
                var myCourse = from m in db.REGISTERs
                               where m.IdUser == UserID && m.Status == true
                               select m.IdCourse;
                ViewBag.RegisteredCourseID = myCourse.ToList();
            }
            return View();
        }

        public ActionResult JoinCourse(int id)
        {
            string CourseID = id.ToString();
            string UserID = Session["UserID"].ToString();
            string queryCommand = "INSERT INTO[dbo].[REGISTER] ([IdCourse] ,[IdUser] ,[Status]) VALUES " +
                                "(" + CourseID + ", " + UserID + ", 1)";
            var query = db.Database.ExecuteSqlCommand(queryCommand);
            return RedirectToAction("Course", "My");
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
                var lesson = from l in db.LESSONs
                             where l.Status == true
                             select l;
                ViewBag.RegisteredCourseID = myCourse.ToList();
                ViewBag.Course = course.ToList();
                ViewBag.Lesson = lesson.ToList().Take(4);
                return View();
            }
            else
            {
                return View("Index");
            }
        }

        public ActionResult GotoCourse(int id)
        {
            Session["Course"] = id;
            return RedirectToAction("CourseDetail", "My");
        }

        public ActionResult CourseDetail(int id, int subid)
        {
            Session["Course"] = id;
            int lessonID = subid;
            var lesson = from l in db.LESSONs
                         where l.Status == true && l.IdCourse == id
                         orderby l.IndexNumber ascending
                         select l;
            var selectedLesson = from s in db.LESSONs
                                 where s.Status == true && s.IdCourse == id && s.Id == lessonID
                                 select s;
            ViewBag.Lesson = lesson.ToList();
            ViewBag.SelectedLesson = selectedLesson.ToList();
            return View();
        }

        public ActionResult LessonDetail(int id)
        {
            int Cid = int.Parse(Session["Course"].ToString());
            int lessonID = id;
            var lesson = from l in db.LESSONs
                         where l.Status == true && l.IdCourse == Cid
                         orderby l.IndexNumber ascending
                         select l;
            var selectedLesson = from s in db.LESSONs
                                 where s.Status == true && s.IdCourse == Cid && s.IndexNumber == lessonID
                                 select s;
            ViewBag.Lesson = lesson.ToList();
            ViewBag.SelectedLesson = selectedLesson.ToList();
            return View("CourseDetail");
        }

        public ActionResult GotoNewLesson(int id)
        {
            Session["Lesson"] = id;
            return RedirectToAction("CourseDetail", "My");
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