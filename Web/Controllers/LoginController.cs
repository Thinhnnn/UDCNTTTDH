using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class LoginController : Controller
    {
        public MyEntities db = new MyEntities();    //khởi tạo biến load database lên
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            if (Session["UserName"] != null)
            {
                return RedirectToAction("Index", "My");
            }
            return View();
        }

        public ActionResult Logout()
        {
            if (Session["UserName"] != null)
            {
                Session["UserName"] = null;
            }
            return RedirectToAction("Index", "My");
        }

        [HttpPost]
        public ActionResult Login(FormCollection fc)
        {
            string email = fc["email"].ToString();
            string password = fc["password"].ToString();
            var user = (from u in db.USERs
                       where u.EMAIL == email
                       select u).ToList();          //lấy thông tin tài khoản có email giống
            if (user.Count == 0)
            {
                ViewBag.EmailError = "Tài khoản không tồn tại!";
                return View();
            }
            else if (user[0].PASSWORD != password)
            {
                ViewBag.PasswordError = "Sai mật khẩu!";
                return View();
            }
            else
            {
                Session["UserName"] = user[0].FULLNAME;
                return RedirectToAction("Index", "My");
            }
        }

        public ActionResult Register()
        {
            //xét điều kiện nếu email đã đăng kí thì không đăng kí nữa
            return View();
        }
        [HttpPost]
        public ActionResult Register(FormCollection fc)
        {
            return RedirectToAction("Index", "My");
        }
    }
}