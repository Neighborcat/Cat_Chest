using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using PagedList;
using PagedList.Mvc;
using StarsResouces.Filter;

namespace StarsResouces.Controllers
{
    [Login]
    public class UserController : Controller
    {
        db_StarsResourcesEntities1 db = new db_StarsResourcesEntities1();
        // GET: User
        
        //个人中心
        public ActionResult UserCenter(int id)
        {
            UserInfo info = db.UserInfo.Find(id);
            Session["info"] = info;
            return View();
        }
        //我要投稿
        public ActionResult Contribution()
        {
            List<Lable> lables = db.Lable.ToList();
            ViewBag.lables = lables;
            return View();
        }
        [HttpPost]
        public ActionResult Contribution(List<HttpPostedFileBase> Picture,string Rname,string Rdescribe,int Rdemand, string CategoryName,string LableName,string Ldescribe,string Linkline,string Lremarks)
        {
            Category cate = db.Category.Where(p=>p.CategoryName == CategoryName).SingleOrDefault();
            Lable lable = db.Lable.Where(p => p.LableName == LableName&&p.CategoryID== cate.CategoryID).SingleOrDefault();
            int cid = cate.CategoryID;
            int laid = lable.LableID;
            int uid = Convert.ToInt32(Session["userid"]);
            Picture pdb = new Picture();
            if (Picture[0]!=null)
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
                Ldescribe= Ldescribe,
                Linkline= Linkline,
                Lremarks= Lremarks
            };
            db.Link.Add(link);
            db.SaveChanges();
            int lid = link.LinkID;
            Resouces resouces = new Resouces() {
                UserID = uid,
                LinkID = lid,
                PictureID = pid,
                CategoryID = cid,
                LableID = laid,
                Releasetime = DateTime.Now,
                Rname= Rname,
                Rdescribe= Rdescribe,
                Rdemand= Rdemand,
                Rstate=1,
                Reading=0
            };
            db.Resouces.Add(resouces);
            db.SaveChanges();
            TempData["sucess"] = "提交成功";
            return View("UserCenter");
        }
        //我的分享
        public ActionResult Myshare(int? page)
        {
            int id = Convert.ToInt32(Session["userid"]);
            List<Resouces> resouces = db.Resouces.Where(p => p.UserID == id).ToList();
            ViewBag.count = resouces.Count();
            int pageNumber = page ?? 1;//页码
            int pageSize = 4;//每页个数
            return View(resouces.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult MyshareEdit(int? id, Resouces resouces)
        {
            if (resouces.ResoucesID == 0)
            {
                Resouces res = db.Resouces.Find(id);
                return View(res);
            }
            else
            {
                db.Entry(resouces).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["sucess"] = "修改成功";
                return RedirectToAction("MyShare");
            }
        }
        public ActionResult MyshareDelete(int?id)
        {
            List<News> rnlist = db.News.Where(p => p.ResoucesID == id).ToList();
            List<Recommend> renlist = db.Recommend.Where(p => p.ResoucesID == id).ToList();
            if (rnlist.Count() > 0 || renlist.Count() > 0)
            {
                TempData["sucess"] = "该资源无法删除。";
                return RedirectToAction("Myshare");
            }
            else
            {
                List<Collection> colist = db.Collection.Where(p => p.ResoucesID == id).ToList();
                if (colist.Count() > 0)
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
                    return RedirectToAction("Myshare");
                }
                else
                {
                    List<Comment> com = db.Comment.Where(p => p.ResoucesID == id).ToList();
                    foreach (var item in com)
                    {
                        db.Comment.Remove(item);
                        db.SaveChanges();
                    }
                    Resouces res = db.Resouces.Find(id);
                    db.Resouces.Remove(res);
                    db.SaveChanges();
                    return RedirectToAction("Myshare");
                }
            }
        }
        //我的收藏
        public ActionResult Collection(int? page)
        {
            int id = Convert.ToInt32(Session["userid"]);
            List<Collection> coll = db.Collection.Where(p=>p.UserID==id).ToList();
            List<Resouces> resouces = new List<Resouces>();
            foreach (var item in coll)
            {
                resouces.Add(item.Resouces);
            }
            ViewBag.resouces = resouces;
            ViewBag.number = resouces.Count();
            return View();
        }
        public ActionResult RemoveCollection(int? id)
        {
            int uid = Convert.ToInt32(Session["userid"]);
            Collection collection = db.Collection.Where(p => p.UserID == uid && p.ResoucesID == id).SingleOrDefault();
            db.Collection.Remove(collection);
            db.SaveChanges();
            return RedirectToAction("Collection");
        }
        //修改资料
        public ActionResult EidtInfo()
        {
            int id = Convert.ToInt32(Session["userid"]);
            UserInfo info = db.UserInfo.Find(id);
            return View(info);
        }
        [HttpPost]
        public ActionResult EidtInfo(UserInfo info, HttpPostedFileBase UserPicture)
        {
            int id = Convert.ToInt32(Session["userid"]);
            if (UserPicture != null)
            {
                //处理图片
                //1,获得文件名称--ie获取文件路径，谷歌获得文件名，通过GetFileName全部化为文件名
                string fileName = Path.GetFileName(UserPicture.FileName);
                //2，判断文件是否为图片
                //string hz = Path.GetExtension(fileName);--获取文件扩展名
                if (fileName.EndsWith("jpg") || fileName.EndsWith("png") || fileName.EndsWith("jpeg") || fileName.EndsWith("gif"))//判断文件名末尾是否为jpg或png等
                {
                    //3，保存图片到项目文件夹中
                    UserPicture.SaveAs(Server.MapPath("~/Content/icon/" + fileName));
                    //4，将图片名，绑定到该用户photo
                    info.UserPicture = fileName;
                }
                else
                {
                    return Content("<script>alert('文件格式不正确')</script>");
                }
            }
                db.Entry(info).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["sucess"] = "修改成功";
                Session["user"] = info;
                return View("UserCenter");
        }
        //评论管理
        public ActionResult Comment(int ?id, int? page)
        {
            List<Comment> clist = db.Comment.Where(p => p.UserID == id).ToList();
            int pageNumber = page ?? 1;//页码
            int pageSize = 7;//每页个数
            return View(clist.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult RemoveCom(int? id)
        {
            int uid = Convert.ToInt32(Session["userid"]);
            Comment com = db.Comment.Find(id);
            db.Comment.Remove(com);
            db.SaveChanges();
            return RedirectToAction("Comment",new { id= uid });
        }
        //修改密码
        public ActionResult ChangePwd()
        {
            int uid = Convert.ToInt32(Session["userid"]);
            UserInfo user = db.UserInfo.Find(uid);
            ViewBag.pwd = user.LoginPwd;
            Session["orldpwd"]= user.LoginPwd;
            return View();
        }
        [HttpPost]
        public ActionResult ChangePwd(string LoginPwd,string OrldLoginPwd)
        {
            string UPwd =Convert.ToString(Session["orldpwd"]);
            if (OrldLoginPwd == UPwd)
            {
                int uid = Convert.ToInt32(Session["userid"]);
                db.Database.ExecuteSqlCommand($"update UserInfo set LoginPwd='{LoginPwd}' where UserID={uid}");
                return Content("<script>alert('修改成功');history.go(-1)</script>");
            }
            else
            {
                TempData["fail"] = "原密码不正确";
                return RedirectToAction("ChangePwd");
            }
        }
        //退出账户
        public ActionResult Exit()
        {
            Session.Clear();
            return RedirectToAction("Index","Home");
        }
    }
}