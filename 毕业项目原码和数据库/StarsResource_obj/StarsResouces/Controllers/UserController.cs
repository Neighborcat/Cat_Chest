using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;

namespace StarsResouces.Controllers
{
    public class UserController : Controller
    {
        db_StarsResourcesEntities2 db = new db_StarsResourcesEntities2();
        // GET: User
        //我的账户
        public ActionResult MainPage(int id)
        {
            UserInfo info = db.UserInfo.Find(id);
            Session["user"] = info;
            return View();
        }
        //个人中心
        public ActionResult UserCenter()
        {
            return View();
        }
        //我要投稿
        public ActionResult Contribution()
        {
            return View();
        }
        //我的分享
        public ActionResult Myshare()
        {
            return View();
        }
        //我的收藏
        public ActionResult Collection()
        {
            return View();
        }
        //修改资料
        public ActionResult EidtInfo()
        {
            return View();
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