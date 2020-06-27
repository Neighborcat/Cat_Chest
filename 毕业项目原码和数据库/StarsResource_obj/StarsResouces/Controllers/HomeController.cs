using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Models;
using PagedList;
using PagedList.Mvc;

namespace StarsResouces.Controllers
{
    public class HomeController : Controller
    {
        db_StarsResourcesEntities2 db = new db_StarsResourcesEntities2();
        public ActionResult Index(int? page)
        {
            List<Resouces> list = db.Resouces.Where(p=>p.Comment.Count>0||p.Reading>10).ToList();
            int pageNumber = page ?? 1;//页码
            int pageSize = 6;//每页个数
            return View(list.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult FuLi(int? page,int ?id)
        {
            if (id==null)
            {
                List<Resouces> fuli = db.Resouces.Where(p => p.CategoryID == 1 || p.LableID == id).ToList();
                ViewBag.count = fuli.Count();
                int pageNumber = page ?? 1;//页码
                int pageSize = 8;//每页个数
                return View(fuli.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                List<Resouces> fuli = db.Resouces.Where(p => p.CategoryID == 1 && p.LableID == id).ToList();
                int pageNumber = page ?? 1;//页码
                int pageSize = 8;//每页个数
                return View(fuli.ToPagedList(pageNumber, pageSize));
            }
            
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
        public ActionResult Details(int id)
        {
            Resouces resouces = db.Resouces.Find(id);
            ViewBag.resouces = resouces;
            int uid = Convert.ToInt32(Session["uid"]);
            var result = db.Collection.Where(p=>p.ResoucesID==id && p.UserID== uid).SingleOrDefault();
            Session["result"] = result;
            return View();
        }
        public ActionResult AddCollection(int ?id)
        {
            int uid = Convert.ToInt32(Session["uid"]);
            if (Session["uid"] != null)
            {
                Collection collection = new Collection()
                {
                    UserID = uid,
                    ResoucesID = id
                };
                db.Collection.Add(collection);
                db.SaveChanges();
                return RedirectToAction("Details",new {id=id });
            }
            else
            {
                return Content("<script>alert('请先登录或注册');history.go(-1);</script>");
            }
        }
        public ActionResult RemoveCollection(int? id)
        {
            int uid = Convert.ToInt32(Session["uid"]);
            Collection collection = db.Collection.Where(p => p.UserID == uid && p.ResoucesID == id).SingleOrDefault();
            db.Collection.Remove(collection);
            db.SaveChanges();
            Session["result"] = null;
            return RedirectToAction("Details", new { id = id });
        }
        public ActionResult Links()
        {
            return View();
        }
        public ActionResult Loading(int id)
        {
            if (id==1)
            {
                ViewBag.link = "https://voice.baidu.com/act/newpneumonia/newpneumonia";
            }
            else if (id == 2)
            {
                ViewBag.link = "https://www.tmall.com/?page_offline=true";
            }
            else if (id == 3)
            {
                ViewBag.link = "https://www.thepaper.cn/";
            }
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
        public ActionResult Exit()
        {
            Session["user"] = null;
            Session["result"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}