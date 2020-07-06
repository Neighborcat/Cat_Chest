using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Models;
using PagedList;
using PagedList.Mvc;
using System.Data.SqlClient;
using System.Dynamic;
using Newtonsoft.Json;

namespace StarsResouces.Controllers
{
    public class HomeController : Controller
    {
        db_StarsResourcesEntities1 db = new db_StarsResourcesEntities1();
        public ActionResult Index(int? page,string search)
        {
            if (search==null)
            {
                List<Resouces> list = db.Resouces.OrderByDescending(p=>p.Reading).ToList();
                int pageNumber = page ?? 1;//页码
                int pageSize = 6;//每页个数
                return View(list.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                var result = db.Resouces.Where(p => p.Rname.Contains(search) || p.Rdescribe.Contains(search)).ToList();
                Session["search"] = result;
                return RedirectToAction("searchResult");
            }
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
        public ActionResult ShouJi(int? page, int? id)
        {
            if (id == null)
            {
                List<Resouces> ShouJi = db.Resouces.Where(p => p.CategoryID == 2 || p.LableID == id).ToList();
                int pageNumber = page ?? 1;//页码
                int pageSize = 8;//每页个数
                return View(ShouJi.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                List<Resouces> ShouJi = db.Resouces.Where(p => p.CategoryID == 2 && p.LableID == id).ToList();
                int pageNumber = page ?? 1;//页码
                int pageSize = 8;//每页个数
                return View(ShouJi.ToPagedList(pageNumber, pageSize));
            }
        }
        public ActionResult XueXi(int? page, int? id)
        {
            if (id == null)
            {
                List<Resouces> XueXi = db.Resouces.Where(p => p.CategoryID == 3 || p.LableID == id).ToList();
                int pageNumber = page ?? 1;//页码
                int pageSize = 8;//每页个数
                return View(XueXi.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                List<Resouces> XueXi = db.Resouces.Where(p => p.CategoryID == 3 && p.LableID == id).ToList();
                int pageNumber = page ?? 1;//页码
                int pageSize = 8;//每页个数
                return View(XueXi.ToPagedList(pageNumber, pageSize));
            }
        }
        public ActionResult JianZhan(int? page, int? id)
        {
            if (id == null)
            {
                List<Resouces> JianZhan = db.Resouces.Where(p => p.CategoryID == 4 || p.LableID == id).ToList();
                int pageNumber = page ?? 1;//页码
                int pageSize = 8;//每页个数
                return View(JianZhan.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                List<Resouces> JianZhan = db.Resouces.Where(p => p.CategoryID == 4 && p.LableID == id).ToList();
                int pageNumber = page ?? 1;//页码
                int pageSize = 8;//每页个数
                return View(JianZhan.ToPagedList(pageNumber, pageSize));
            }
        }
        public ActionResult DianNao(int? page, int? id)
        {
            if (id == null)
            {
                List<Resouces> DianNao = db.Resouces.Where(p => p.CategoryID == 5 || p.LableID == id).ToList();
                int pageNumber = page ?? 1;//页码
                int pageSize = 8;//每页个数
                return View(DianNao.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                List<Resouces> DianNao = db.Resouces.Where(p => p.CategoryID == 5 && p.LableID == id).ToList();
                int pageNumber = page ?? 1;//页码
                int pageSize = 8;//每页个数
                return View(DianNao.ToPagedList(pageNumber, pageSize));
            }
        }
        public ActionResult Details(int? page, int id)
        {
            db.Database.ExecuteSqlCommand($"update Resouces set Reading+=1 where ResoucesID={id}");
            Resouces resouces = db.Resouces.Find(id);
            ViewBag.resouces = resouces;
            int uid = Convert.ToInt32(Session["uid"]);
            Session["rid"]= id;
            var result = db.Collection.Where(p=>p.ResoucesID==id && p.UserID== uid).SingleOrDefault();
            Session["result"] = result;
            int pageNumber = page ?? 1;//页码
            int pageSize = 6;//每页个数
            List<Comment> com = db.Comment.Where(p => p.ResoucesID == id).OrderByDescending(p=>p.Time).ToList();
            ViewBag.com = com.ToPagedList(pageNumber, pageSize);
            ViewBag.uid = resouces.UserID;
            return View();
        }
        [HttpPost]
        public ActionResult Comment(string content)
        {
            int id = Convert.ToInt32(Session["rid"]);
            int uid = Convert.ToInt32(Session["uid"]);
            Comment com = new Comment()
            {
                UserID = uid,
                Content = content,
                ResoucesID = id,
                Time = DateTime.Now
            };
            db.Comment.Add(com);
            db.SaveChanges();
            return RedirectToAction("Details","Home",new { id= id });
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
        public ActionResult Links(int ?id)
        {
            int rid = Convert.ToInt32(Session["rid"]);
            int uid = Convert.ToInt32(Session["uid"]);
            Resouces resouces = db.Resouces.Where(p => p.ResoucesID == rid).SingleOrDefault();
            UserInfo info= db.UserInfo.Where(p => p.UserID == uid).SingleOrDefault();
            if (uid==0)
            {
                return Content("<script>alert('请先登录！');history.go(-1)</script>");
            }
            else if (info.integral< resouces.Rdemand)
            {
                TempData["nofull"] = "您的积分不足";
                return RedirectToAction("Details", new { id = rid });
            }
            else
            {
                db.Database.ExecuteSqlCommand($"update UserInfo set integral-={resouces.Rdemand} where UserID={uid}");
                Link link = db.Link.Where(p => p.LinkID == id).SingleOrDefault();
                return View(link);
            }
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
        public ActionResult searchResult(int ?page)
        {
            List<Resouces> list = Session["search"] as List<Resouces>;
            int pageNumber = page ?? 1;//页码
            int pageSize = 8;//每页个数
            return View(list.ToPagedList(pageNumber, pageSize));
        }
        [HttpPost]
        public ActionResult LoginModal(UserInfo user)
        {
                var result = db.UserInfo.Where(p => p.LoginName == user.LoginName && p.LoginPwd == user.LoginPwd).SingleOrDefault();
                if (result!=null)
                {
                    Session["user"] = result;
                    Session["userid"] = result.UserID;
                    return RedirectToAction("Index");
                }
                else
                {
                    return Content("<script>alert('账号或密码输入错误！');history.go(-1)</script>");
                }
        }
        [HttpPost]
        public ActionResult RegisterModal(string LoginName,string LoginPwd,string E_mail)
        {
            var result = db.UserInfo.Where(p => p.LoginName == LoginName).SingleOrDefault();
            if (result != null)
            {
                return Content("<script>alert('该账号已被注册!');history.go(-1)</script>");
            }
            else
            {
                UserInfo info = new UserInfo()
                {
                    RegistrationTime=DateTime.Now,
                    LoginName = LoginName,
                    LoginPwd = LoginPwd,
                    E_mail = E_mail,
                    UserName = "萌新求带",
                    UserSex="0",
                    Userdescribe="懒得很，什么都没有~",
                    integral=5,
                    UserState=0,
                    UserPicture= "default.jpg"
                };
                db.UserInfo.Add(info);
                db.SaveChanges();
                return Content("<script>alert('注册成功!');history.go(-1)</script>");
            }
        }
        public ActionResult Exit()
        {
            Session["uid"] = null;
            Session["user"] = null;
            Session["result"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}