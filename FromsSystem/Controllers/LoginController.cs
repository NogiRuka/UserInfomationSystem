using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FromsSystem.Controllers
{
    public class LoginController : BaseController
    {
        // GET: Login
        //登录界面跳转
        public ActionResult Login()
        {
            return View();
        }
        //登录信息校验
        public ActionResult CheckUser()
        {
            string username = Request["username"].ToString();
            string password = Request["password"].ToString();

            if (username == "admin" && password == "admin")
            {
                Session["isLogin"] = "login";
                return SuccessRes("登陆成功");
            }
            else
            {
                return ErrorRes("用户名或密码错误");
            }
        }

    }
}