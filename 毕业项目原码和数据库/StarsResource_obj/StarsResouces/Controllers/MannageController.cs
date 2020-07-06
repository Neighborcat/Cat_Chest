using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using PagedList;
using PagedList.Mvc;

namespace Mannage.Controllers
{
    public class MannageController : Controller
    {
        db_StarsResourcesEntities1 db = new db_StarsResourcesEntities1();
        public ActionResult Index()
        {
            
            return View();
        }
        public ActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddUser(string LoginName, string LoginPwd, string E_mail,string UserName,string UserSex,int UserState)
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
                    RegistrationTime = DateTime.Now,
                    LoginName = LoginName,
                    LoginPwd = LoginPwd,
                    E_mail = E_mail,
                    UserName = UserName,
                    UserSex = UserSex,
                    Userdescribe = "懒得很，什么都没有~",
                    integral = 5,
                    UserState = UserState,
                    UserPicture = "default.jpg"
                };
                db.UserInfo.Add(info);
                db.SaveChanges();
                return Content("<script>alert('添加成功!');history.go(-1)</script>");
            }
        }
        public ActionResult Users(int? page,string searcher)
        {
            if (searcher==null)
            {
                List<UserInfo> ulist = db.UserInfo.ToList();
                int pageNumber = page ?? 1;//页码
                int pageSize = 6;//每页个数
                return View(ulist.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                List<UserInfo> ulist = db.UserInfo.Where(p=>p.LoginName.Contains(searcher)||p.UserName.Contains(searcher)).ToList();
                int pageNumber = page ?? 1;//页码
                int pageSize = 6;//每页个数
                return View(ulist.ToPagedList(pageNumber, pageSize));
            }
            
        }
        public ActionResult AddRes()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddRes(List<HttpPostedFileBase> Picture, string Rname, string Rdescribe, int Rdemand, string CategoryName, string LableName, string Ldescribe, string Linkline, string Lremarks)
        {
            Category cate = db.Category.Where(p => p.CategoryName == CategoryName).SingleOrDefault();
            Lable lable = db.Lable.Where(p => p.LableName == LableName && p.CategoryID == cate.CategoryID).SingleOrDefault();
            int cid = cate.CategoryID;
            int laid = lable.LableID;
            int uid = Convert.ToInt32(Session["userid"]);
            Picture pdb = new Picture();
            if (Picture[0] != null)
            {
                for (int i = 0; i < Picture.Count(); i++)
                {
                    string fileName = Path.GetFileName(Picture[i].FileName);
                    if (fileName.EndsWith("jpg") || fileName.EndsWith("png") || fileName.EndsWith("jpeg") || fileName.EndsWith("gif"))//判断文件名末尾是否为jpg或png等
                    {
                        Picture[i].SaveAs(Server.MapPath("~/Content/Resouces/Images/" + fileName));
                        if (i == Picture.Count() - 5)
                        {
                            pdb.Picture_e = fileName;
                        }
                        if (i == Picture.Count() - 4)
                        {
                            pdb.Picture_d = fileName;
                        }
                        if (i == Picture.Count() - 3)
                        {
                            pdb.Picture_c = fileName;
                        }
                        if (i == Picture.Count() - 2)
                        {
                            pdb.Picture_b = fileName;
                        }
                        if (i == Picture.Count() - 1)
                        {
                            pdb.Picture_a = fileName;
                        }
                    }
                    else
                    {
                        return Content("<script>alert('请选择正确的文件格式')</script>");
                    }
                }
            }
            else
            {
                pdb.Picture_a = null;
                pdb.Picture_b = null;
                pdb.Picture_c = null;
                pdb.Picture_d = null;
                pdb.Picture_e = null;
            }
            db.Picture.Add(pdb);
            db.SaveChanges();
            int pid = pdb.PictureID;
            Link link = new Link()
            {
                Ldescribe = Ldescribe,
                Linkline = Linkline,
                Lremarks = Lremarks
            };
            db.Link.Add(link);
            db.SaveChanges();
            int lid = link.LinkID;
            Resouces resouces = new Resouces()
            {
                UserID = 1,
                LinkID = lid,
                PictureID = pid,
                CategoryID = cid,
                LableID = laid,
                Releasetime = DateTime.Now,
                Rname = Rname,
                Rdescribe = Rdescribe,
                Rdemand = Rdemand,
                Rstate = 0,
                Reading = 0
            };
            db.Resouces.Add(resouces);
            db.SaveChanges();
            TempData["saozhu"] = "添加成功IIIIII，骚猪";
            return View();
        }
        public ActionResult Resouces(int? page, string searcher,int ?CategoryID,int ?Rstate)
        {
            List<Category> category = db.Category.ToList();
            ViewBag.cate = category;
            //没有查询条件
            if (searcher == null&& CategoryID == null&& Rstate==null)
            {
                List<Resouces> rlist = db.Resouces.ToList();
                int pageNumber = page ?? 1;//页码
                int pageSize = 6;//每页个数
                return View(rlist.ToPagedList(pageNumber, pageSize));
            }
            //只有查询内容
            else if(searcher != null && CategoryID == null && Rstate == null)
            {
                List<Resouces> rlist = db.Resouces.Where(p => p.Rname.Contains(searcher) || p.Rdescribe.Contains(searcher)).ToList();
                int pageNumber = page ?? 1;//页码
                int pageSize = 6;//每页个数
                return View(rlist.ToPagedList(pageNumber, pageSize));
            }
            //既有内容又有类别
            else if (searcher != null && CategoryID != null && Rstate == null)
            {
                List<Resouces> rlist = db.Resouces.Where(p => p.Rname.Contains(searcher) && p.Rdescribe.Contains(searcher)&&p.CategoryID== CategoryID).ToList();
                int pageNumber = page ?? 1;//页码
                int pageSize = 6;//每页个数
                return View(rlist.ToPagedList(pageNumber, pageSize));
            }
            //三个条件都有
            else if (searcher != null && CategoryID != null && Rstate != null)
            {
                List<Resouces> rlist = db.Resouces.Where(p => p.Rname.Contains(searcher) && p.Rdescribe.Contains(searcher) && p.CategoryID == CategoryID&&p.Rstate== Rstate).ToList();
                int pageNumber = page ?? 1;//页码
                int pageSize = 6;//每页个数
                return View(rlist.ToPagedList(pageNumber, pageSize));
            }
            //只有类别
            else if (searcher == null && CategoryID != null && Rstate == null)
            {
                List<Resouces> rlist = db.Resouces.Where(p =>p.CategoryID == CategoryID).ToList();
                int pageNumber = page ?? 1;//页码
                int pageSize = 6;//每页个数
                return View(rlist.ToPagedList(pageNumber, pageSize));
            }
            //只有状态
            else if (searcher == null && CategoryID == null && Rstate != null)
            {
                List<Resouces> rlist = db.Resouces.Where(p =>p.Rstate == Rstate).ToList();
                int pageNumber = page ?? 1;//页码
                int pageSize = 6;//每页个数
                return View(rlist.ToPagedList(pageNumber, pageSize));
            }
            //既有内容又有状态
            else if (searcher != null && CategoryID == null && Rstate != null)
            {
                List<Resouces> rlist = db.Resouces.Where(p => p.Rname.Contains(searcher) && p.Rdescribe.Contains(searcher)&& p.Rstate == Rstate).ToList();
                int pageNumber = page ?? 1;//页码
                int pageSize = 6;//每页个数
                return View(rlist.ToPagedList(pageNumber, pageSize));
            }
            //没有查询内容有类别和状态
            else
            {
                List<Resouces> rlist = db.Resouces.Where(p =>p.CategoryID == CategoryID && p.Rstate == Rstate).ToList();
                int pageNumber = page ?? 1;//页码
                int pageSize = 6;//每页个数
                return View(rlist.ToPagedList(pageNumber, pageSize));
            }
        }
        public ActionResult RComment()
        {
            return View();
        }
        public ActionResult Advertisement()
        {

            return View();
        }
        public ActionResult EditAdver()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string LoginName, string LoginPwd)
        {
            Administrators adm = db.Administrators.Where(p=>p.LoginName== LoginName && p.LoginPwd== LoginPwd).FirstOrDefault();
            if (adm!=null)
            {
                Session["result"] = adm;
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.error = "账号或密码错误";
                return View();
            }
        }
        public ActionResult Exit()
        {
            Session["result"] = null;
            return RedirectToAction("Index");
        }
    }
}