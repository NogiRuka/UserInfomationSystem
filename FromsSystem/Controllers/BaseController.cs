using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;

namespace FromsSystem.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected ActionResult SuccessRes(object data=null)
        {
            return Json(new JsonRes(0, "操作成功", data), JsonRequestBehavior.AllowGet);
        }
        protected ActionResult ErrorRes( object data = null)
        {
            return Json(new JsonRes(1, "操作失败", data), JsonRequestBehavior.AllowGet);
        }
    }
    public class JsonRes
    {
        public JsonRes(int code, 
            string msg, object data = null)
        {
            this.code = code;
            this.msg = msg;
            this.data = data;
        }
        public int code { get; set; }
        public string msg { get; set; }
        public object data { get; set; }
    }


}