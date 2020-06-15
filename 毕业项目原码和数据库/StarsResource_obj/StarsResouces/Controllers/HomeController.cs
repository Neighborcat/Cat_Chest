using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Models;


namespace StarsResouces.Controllers
{
    public class HomeController : Controller
    {
        db_StarsResourcesEntities2 db = new db_StarsResourcesEntities2();
        public ActionResult Index()
        {
            return View(db.Resouces.ToList());
        }
        public ActionResult FuLi()
        {
            return View();
        }
        public ActionResult ShouJi()
        {
            return View();
        }
        public ActionResult XueXi()
        {
            return View();
        }
        public ActionResult JianZhan()
        {
            return View();
        }
        public ActionResult DianNao()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginModal(UserInfo user)
        {
                var result = db.UserInfo.Where(p => p.LoginName == user.LoginName && p.LoginPwd == user.LoginPwd).SingleOrDefault();
                if (result!=null)
                {
                    Session["user"] = result;
                    return RedirectToAction("Index");
                }
                else
                {
                    return Content("账号或密码输入错误！");
                }
        }

    }
}