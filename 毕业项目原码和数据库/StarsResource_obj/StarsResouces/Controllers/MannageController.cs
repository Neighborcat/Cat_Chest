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
        public ActionResult UserDelete(int ?id)
        {
            List<Resouces> urlist = db.Resouces.Where(p => p.UserID == id).ToList();
            if (urlist.Count()>0)
            {
                TempData["sucess"] = "该用户无法删除。";
                return RedirectToAction("Users");
            }
            else
            {
                List<Collection> colist = db.Collection.Where(p => p.UserID == id).ToList();
                if (colist.Count()==0)
                {
                    UserInfo user = db.UserInfo.Find(id);
                    db.UserInfo.Remove(user);
                    db.SaveChanges();
                    return RedirectToAction("Users");
                }
                else
                {

                    for (int i = 0; i < colist.Count(); i++)
                    {
                        db.Collection.Remove(colist[i]);
                        db.SaveChanges();
                    }
                    UserInfo user = db.UserInfo.Find(id);
                    db.UserInfo.Remove(user);
                    db.SaveChanges();
                    return RedirectToAction("Users");
                }
            }
        }
        public ActionResult UserDetails(int ?id,UserInfo info)
        {
            if (info.UserID == 0)
            {
                UserInfo user = db.UserInfo.Find(id);
                return View(user);
            }
            else
            {
                db.Entry(info).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["sucess"] = "审批成功";
                return RedirectToAction("Users");
            }
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
        public ActionResult Users(int ?UserState, string searcher, int pageIndex = 1, int pageCount = 10)
        {
            if (searcher==null&& UserState==null)
            {
                int tatalCount = db.UserInfo.OrderBy(p => p.UserID).Count();
                double tatalPage = Math.Ceiling(tatalCount / (double)pageCount);
                List<UserInfo> uList = db.UserInfo.OrderBy(p => p.UserID).Skip((pageIndex - 1) * pageCount).Take(pageCount).ToList();
                //当前页
                ViewBag.pageIndex = pageIndex;
                //每页行数
                ViewBag.pageCount = pageCount;
                //总行数
                ViewBag.tatalCount = tatalCount;
                //总页数
                ViewBag.tatalPage = tatalPage;
                return View(uList);
            }
            else if(searcher != null && UserState == null)
            {
                int tatalCount = db.UserInfo.Where(p => p.LoginName.Contains(searcher) || p.UserName.Contains(searcher)).OrderBy(p => p.UserID).Count();
                double tatalPage = Math.Ceiling(tatalCount / (double)pageCount);
                List<UserInfo> uList = db.UserInfo.Where(p => p.LoginName.Contains(searcher) || p.UserName.Contains(searcher)).OrderBy(p => p.UserID).Skip((pageIndex - 1) * pageCount).Take(pageCount).ToList();
                //当前页
                ViewBag.pageIndex = pageIndex;
                //每页行数
                ViewBag.pageCount = pageCount;
                //总行数
                ViewBag.tatalCount = tatalCount;
                //总页数
                ViewBag.tatalPage = tatalPage;
                return View(uList);
            }
            else if (searcher == null || UserState != null)
            {
                int tatalCount = db.UserInfo.Where(p => p.UserState==UserState).OrderBy(p => p.UserID).Count();
                double tatalPage = Math.Ceiling(tatalCount / (double)pageCount);
                List<UserInfo> uList = db.UserInfo.Where(p => p.UserState == UserState).OrderBy(p => p.UserID).Skip((pageIndex - 1) * pageCount).Take(pageCount).ToList();
                //当前页
                ViewBag.pageIndex = pageIndex;
                //每页行数
                ViewBag.pageCount = pageCount;
                //总行数
                ViewBag.tatalCount = tatalCount;
                //总页数
                ViewBag.tatalPage = tatalPage;
                return View(uList);
            }
            else
            {
                int tatalCount = db.UserInfo.Where(p => p.LoginName.Contains(searcher) || p.UserName.Contains(searcher) && p.UserState == UserState).OrderBy(p => p.UserID).Count();
                double tatalPage = Math.Ceiling(tatalCount / (double)pageCount);
                List<UserInfo> uList = db.UserInfo.Where(p => p.LoginName.Contains(searcher) || p.UserName.Contains(searcher) && p.UserState == UserState).OrderBy(p => p.UserID).Skip((pageIndex - 1) * pageCount).Take(pageCount).ToList();
                //当前页
                ViewBag.pageIndex = pageIndex;
                //每页行数
                ViewBag.pageCount = pageCount;
                //总行数
                ViewBag.tatalCount = tatalCount;
                //总页数
                ViewBag.tatalPage = tatalPage;
                return View(uList);
            }


        }
        public ActionResult AddRes()
        {
            return View();
        }
        public ActionResult ResDetails(int ?id,Resouces resouces)
        {
            if (resouces.ResoucesID == 0)
            {
                Resouces res = db.Resouces.Find(id);
                return View(res);
            }
            else
            {
                db.Entry(resouces).State= System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["sucess"] = "审批成功";
                return RedirectToAction("Resouces");
            }
            
        }
        public ActionResult ResDelete(int ?id)
        {
            List<News> rnlist = db.News.Where(p => p.ResoucesID == id).ToList();
            List<Recommend> renlist = db.Recommend.Where(p => p.ResoucesID == id).ToList();
            if (rnlist.Count() > 0|| renlist.Count() > 0)
            {
                TempData["sucess"] = "该资源无法删除。";
                return RedirectToAction("Resouces");
            }
            else
            {
                List<Collection> colist = db.Collection.Where(p => p.ResoucesID == id).ToList();
                if (colist.Count()>0)
                {
                    foreach (var item in colist)
                    {
                        db.Collection.Remove(item);
                        db.SaveChanges();
                    }
                    List<Comment> com = db.Comment.Where(p => p.ResoucesID == id).ToList();
                    foreach (var item in com)
                    {
                        db.Comment.Remove(item);
                        db.SaveChanges();
                    }
                    Resouces res = db.Resouces.Find(id);
                    db.Resouces.Remove(res);
                    db.SaveChanges();
                    return RedirectToAction("Resouces");
                }
                else
                {
                    List<Comment> com = db.Comment.Where(p=>p.ResoucesID==id).ToList();
                    foreach (var item in com)
                    {
                        db.Comment.Remove(item);
                        db.SaveChanges();
                    }
                    Resouces res = db.Resouces.Find(id);
                    db.Resouces.Remove(res);
                    db.SaveChanges();
                    return RedirectToAction("Resouces");
                }
            }
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
            TempData["saozhu"] = "添加成功";
            return View();
        }
        public ActionResult Resouces(int? page, string searcher,int ?CategoryID,int ?Rstate,int pageIndex = 1, int pageCount = 10)
        {
            List<Category> category = db.Category.ToList();
            ViewBag.cate = category;
            //没有查询条件
            if (searcher == null&& CategoryID == null&& Rstate==null)
            {
                //带分页
                int tatalCount = db.Resouces.OrderByDescending(p => p.ResoucesID).Count();
                double tatalPage = Math.Ceiling(tatalCount / (double)pageCount);
                List<Resouces> uList = db.Resouces.OrderByDescending(p => p.ResoucesID).Skip((pageIndex - 1) * pageCount).Take(pageCount).ToList();
                //当前页
                ViewBag.pageIndex = pageIndex;
                //每页行数
                ViewBag.pageCount = pageCount;
                //总行数
                ViewBag.tatalCount = tatalCount;
                //总页数
                ViewBag.tatalPage = tatalPage;
                return View(uList);

            }
            //只有查询内容
            else if(searcher != null && CategoryID == null && Rstate == null)
            {
                int tatalCount = db.Resouces.Where(p => p.Rname.Contains(searcher) || p.Rdescribe.Contains(searcher)).OrderByDescending(p => p.ResoucesID).Count();
                double tatalPage = Math.Ceiling(tatalCount / (double)pageCount);
                List<Resouces> uList = db.Resouces.Where(p => p.Rname.Contains(searcher) || p.Rdescribe.Contains(searcher)).OrderByDescending(p => p.ResoucesID).Skip((pageIndex - 1) * pageCount).Take(pageCount).ToList();
                //当前页
                ViewBag.pageIndex = pageIndex;
                //每页行数
                ViewBag.pageCount = pageCount;
                //总行数
                ViewBag.tatalCount = tatalCount;
                //总页数
                ViewBag.tatalPage = tatalPage;
                return View(uList);
            }
            //既有内容又有类别
            else if (searcher != null && CategoryID != null && Rstate == null)
            {
                int tatalCount = db.Resouces.Where(p => p.Rname.Contains(searcher) && p.Rdescribe.Contains(searcher) && p.CategoryID == CategoryID).OrderByDescending(p => p.ResoucesID).Count();
                double tatalPage = Math.Ceiling(tatalCount / (double)pageCount);
                List<Resouces> uList = db.Resouces.Where(p => p.Rname.Contains(searcher) && p.Rdescribe.Contains(searcher) && p.CategoryID == CategoryID).OrderByDescending(p => p.ResoucesID).Skip((pageIndex - 1) * pageCount).Take(pageCount).ToList();
                //当前页
                ViewBag.pageIndex = pageIndex;
                //每页行数
                ViewBag.pageCount = pageCount;
                //总行数
                ViewBag.tatalCount = tatalCount;
                //总页数
                ViewBag.tatalPage = tatalPage;
                return View(uList);
            }
            //三个条件都有
            else if (searcher != null && CategoryID != null && Rstate != null)
            {
                int tatalCount = db.Resouces.Where(p => p.Rname.Contains(searcher) && p.Rdescribe.Contains(searcher) && p.CategoryID == CategoryID && p.Rstate == Rstate).OrderByDescending(p => p.ResoucesID).Count();
                double tatalPage = Math.Ceiling(tatalCount / (double)pageCount);
                List<Resouces> uList = db.Resouces.Where(p => p.Rname.Contains(searcher) && p.Rdescribe.Contains(searcher) && p.CategoryID == CategoryID && p.Rstate == Rstate).OrderByDescending(p => p.ResoucesID).Skip((pageIndex - 1) * pageCount).Take(pageCount).ToList();
                //当前页
                ViewBag.pageIndex = pageIndex;
                //每页行数
                ViewBag.pageCount = pageCount;
                //总行数
                ViewBag.tatalCount = tatalCount;
                //总页数
                ViewBag.tatalPage = tatalPage;
                return View(uList);
            }
            //只有类别
            else if (searcher == null && CategoryID != null && Rstate == null)
            {
                int tatalCount = db.Resouces.Where(p => p.CategoryID == CategoryID).OrderByDescending(p => p.ResoucesID).Count();
                double tatalPage = Math.Ceiling(tatalCount / (double)pageCount);
                List<Resouces> uList = db.Resouces.Where(p => p.CategoryID == CategoryID).OrderByDescending(p => p.ResoucesID).Skip((pageIndex - 1) * pageCount).Take(pageCount).ToList();
                //当前页
                ViewBag.pageIndex = pageIndex;
                //每页行数
                ViewBag.pageCount = pageCount;
                //总行数
                ViewBag.tatalCount = tatalCount;
                //总页数
                ViewBag.tatalPage = tatalPage;
                return View(uList);
            }
            //只有状态
            else if (searcher == null && CategoryID == null && Rstate != null)
            {
                int tatalCount = db.Resouces.Where(p => p.Rstate == Rstate).OrderByDescending(p => p.ResoucesID).Count();
                double tatalPage = Math.Ceiling(tatalCount / (double)pageCount);
                List<Resouces> uList = db.Resouces.Where(p => p.Rstate == Rstate).OrderByDescending(p => p.ResoucesID).Skip((pageIndex - 1) * pageCount).Take(pageCount).ToList();
                //当前页
                ViewBag.pageIndex = pageIndex;
                //每页行数
                ViewBag.pageCount = pageCount;
                //总行数
                ViewBag.tatalCount = tatalCount;
                //总页数
                ViewBag.tatalPage = tatalPage;
                return View(uList);
            }
            //既有内容又有状态
            else if (searcher != null && CategoryID == null && Rstate != null)
            {
                int tatalCount = db.Resouces.Where(p => p.Rname.Contains(searcher) && p.Rdescribe.Contains(searcher) && p.Rstate == Rstate).OrderByDescending(p => p.ResoucesID).Count();
                double tatalPage = Math.Ceiling(tatalCount / (double)pageCount);
                List<Resouces> uList = db.Resouces.Where(p => p.Rname.Contains(searcher) && p.Rdescribe.Contains(searcher) && p.Rstate == Rstate).OrderByDescending(p => p.ResoucesID).Skip((pageIndex - 1) * pageCount).Take(pageCount).ToList();
                //当前页
                ViewBag.pageIndex = pageIndex;
                //每页行数
                ViewBag.pageCount = pageCount;
                //总行数
                ViewBag.tatalCount = tatalCount;
                //总页数
                ViewBag.tatalPage = tatalPage;
                return View(uList);
            }
            //没有查询内容有类别和状态
            else
            {
                int tatalCount = db.Resouces.Where(p => p.CategoryID == CategoryID && p.Rstate == Rstate).OrderByDescending(p => p.ResoucesID).Count();
                double tatalPage = Math.Ceiling(tatalCount / (double)pageCount);
                List<Resouces> uList = db.Resouces.Where(p => p.CategoryID == CategoryID && p.Rstate == Rstate).OrderByDescending(p => p.ResoucesID).Skip((pageIndex - 1) * pageCount).Take(pageCount).ToList();
                //当前页
                ViewBag.pageIndex = pageIndex;
                //每页行数
                ViewBag.pageCount = pageCount;
                //总行数
                ViewBag.tatalCount = tatalCount;
                //总页数
                ViewBag.tatalPage = tatalPage;
                return View(uList);
            }
        }
        public ActionResult RComment (int pageIndex = 1, int pageCount = 10)
        {
            int tatalCount = db.Resouces.Where(p=>p.Comment.Count()>0).OrderByDescending(p => p.ResoucesID).Count();
            double tatalPage = Math.Ceiling(tatalCount / (double)pageCount);
            List<Resouces> uList = db.Resouces.Where(p => p.Comment.Count() > 0).OrderBy(p => p.ResoucesID).Skip((pageIndex - 1) * pageCount).Take(pageCount).ToList();
            //当前页
            ViewBag.pageIndex = pageIndex;
            //每页行数
            ViewBag.pageCount = pageCount;
            //总行数
            ViewBag.tatalCount = tatalCount;
            //总页数
            ViewBag.tatalPage = tatalPage;
            return View(uList);
        }
        public ActionResult ComDetails(int? id,int pageIndex = 1, int pageCount = 10)
        {
            Session["rid"] = id;
            int tatalCount = db.Comment.Where(p => p.ResoucesID==id).OrderByDescending(p => p.ResoucesID).Count();
            double tatalPage = Math.Ceiling(tatalCount / (double)pageCount);
            List<Comment> uList = db.Comment.Where(p => p.ResoucesID == id).OrderBy(p => p.ResoucesID).Skip((pageIndex - 1) * pageCount).Take(pageCount).ToList();
            //当前页
            ViewBag.pageIndex = pageIndex;
            //每页行数
            ViewBag.pageCount = pageCount;
            //总行数
            ViewBag.tatalCount = tatalCount;
            //总页数
            ViewBag.tatalPage = tatalPage;
            return View(uList);
        }
        public ActionResult RemoveCom(int? id)
        {
            int rid = Convert.ToInt32(Session["rid"]);
            Comment com = db.Comment.Find(id);
            db.Comment.Remove(com);
            db.SaveChanges();
            return RedirectToAction("ComDetails",new { id= rid });
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
                Session["Admin"] = adm;
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
            Session["Admin"] = null;
            return RedirectToAction("Index");
        }
    }
}