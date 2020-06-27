using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using PagedList;
using PagedList.Mvc;

namespace StarsResouces.Controllers
{
    public class UserController : Controller
    {
        db_StarsResourcesEntities2 db = new db_StarsResourcesEntities2();
        // GET: User
        //个人中心
        public ActionResult UserCenter(int id)
        {
            UserInfo info = db.UserInfo.Find(id);
            ViewBag.info = info;
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
        public ActionResult Contribution(int ?id)
        {
            //获取图片
            string msg = "";
            if (Request.Files.Count > 0) //request.files.count客户端传过来几个文件
            {
                try
                {
                    HttpPostedFileBase mypost = Request.Files[0];//取客户端传过来多个文件的第一个
                    string fileName = Path.GetFileName(mypost.FileName);//通过文件流取文件名称
                    string serverpath = Server.MapPath(
                        string.Format("~/Content/Resouces/{0}", "Images")); //获取要存入的服务器上的地址
                    string path = Path.Combine(serverpath, fileName); //将定义的服务器路径和文件名结合，形成完整路径
                    mypost.SaveAs(path); //将文件保存
                    //msg = "文件上传成功！";
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                }
            }
            else
            {
                msg = "请选择文件";
            }
            return Json(msg);
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
        //我的收藏
        public ActionResult Collection()
        {
            return View();
        }
        //修改资料
        public ActionResult EidtInfo()
        {
            int id = Convert.ToInt32(Session["userid"]);
            UserInfo info = db.UserInfo.Find(id);
            return View(info);
        }
        //评论管理
        public ActionResult Comment()
        {
            return View();
        }
        //修改密码
        public ActionResult ChangePwd()
        {
            return View();
        }
        //退出账户
        public ActionResult Exit()
        {
            Session["user"] = null;
            return RedirectToAction("Index","Home");
        }
    }
}