using FromsSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Threading;

namespace FromsSystem.Controllers
{
    public class FormController : BaseController
    {
        // GET: Form
        public ActionResult Index()
        {
            if(Session["isLogin"] == null ||
                Session["isLogin"].ToString() != "login")
            {
                Response.Redirect("../Login/Login");
                
            }
            return View();

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddUser(form user)
        {
            var formProvider = new FormProvider();

            form fEdit =  formProvider.Select().FirstOrDefault(item => item.id == user.id);

            if (fEdit == null)
            {
                form f = new form();
                f.name = user.name;
                f.age = user.age;
                f.height = user.height;
                f.weight = user.weight;
                f.school = user.school;
                f.profile = user.profile == null ? "这个人很神秘，什么都没有写" : user.profile;
                f.avatar = user.avatar == null ? "/Images/avatar.png" : user.avatar;
                f.createTime = DateTime.Now;
                f.updateTime = DateTime.Now;
                int count = formProvider.Insert(f);
                if (count > 0)
                {
                    return SuccessRes("添加成功");
                }
                else
                {
                    return ErrorRes("添加失败");
                }
            }
            else
            {
                Session["userId"] = user.id;
                fEdit.name = user.name;
                fEdit.age = user.age;
                fEdit.height = user.height;
                fEdit.weight = user.weight;
                fEdit.school = user.school;
                fEdit.profile = user.profile == null ? "这个人很神秘，什么都没有写" : user.profile;
                fEdit.avatar = user.avatar == null ? "/Images/avatar.png" : user.avatar;
                fEdit.createTime = user.createTime == null ? DateTime.Now : user.createTime;
                fEdit.updateTime = user.updateTime == null ? DateTime.Now : user.updateTime;
                int count = formProvider.Update(fEdit);
                if (count > 0)
                {
                    return SuccessRes("修改成功");
                }
                else
                {
                    return ErrorRes("修改失败");
                }
            }
        }

        public JsonResult Upload()
        {
            var result = new Dictionary<string, object>();
            result["code"] = -1;
            Dictionary<string, object> data = new Dictionary<string, object>();
            try
            {
                //string editor = Request["e"];//e=1，表示editor
                string t = Request["t"];//类型，img，表示图片类的，file表示文件类
                //string selfPath = Request["p"];//自定义文件夹名称

                var file = Request.Files[0]; //获取选中文件  
                Stream stream = file.InputStream;    //将文件转为流 
                string ext = file.FileName.Substring(file.FileName.LastIndexOf('.'));

                //文件保存位置及命名，精确到毫秒并附带一组随机数，防止文件重名，数据库保存路径为此变量  
                string dir = "/Images/uploads/";
                string rootPath = Server.MapPath(dir);
                if (!Directory.Exists(rootPath))
                    Directory.CreateDirectory(rootPath);
                Random ran = new Random((int)DateTime.Now.Ticks);//利用时间种子解决伪随机数短时间重复问题  
                string fileName = DateTime.Now.ToString("mmssms") + ran.Next(99999);
                string serverPath = dir + fileName + ext;

                //路径映射为绝对路径  
                string path = Server.MapPath(serverPath);
                if (t == "img")
                {
                    System.Drawing.Image img = System.Drawing.Image.FromStream(stream);//将流中的图片转换为Image图片对象  
                    img.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);//图片保存，JPEG格式图片较小  
                }
                else
                {
                    file.SaveAs(path);
                    data.Add("src", serverPath);
                    data.Add("title", fileName);
                    result["data"] = data;
                    result["code"] = 0;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                result["code"] = -1;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UploadSummer()
        {
            var result = new Dictionary<string, object>();
            result["code"] = -1;
            Dictionary<string, object> data = new Dictionary<string, object>();
            try
            {
                //string editor = Request["e"];//e=1，表示editor
                string t = Request["t"];//类型，img，表示图片类的，file表示文件类
                //string selfPath = Request["p"];//自定义文件夹名称

                var file = Request.Files[0]; //获取选中文件  
                Stream stream = file.InputStream;    //将文件转为流 
                string ext = file.FileName.Substring(file.FileName.LastIndexOf('.'));

                //文件保存位置及命名，精确到毫秒并附带一组随机数，防止文件重名，数据库保存路径为此变量  
                string dir = "/Images/uploads/";
                string rootPath = Server.MapPath(dir);
                if (!Directory.Exists(rootPath))
                    Directory.CreateDirectory(rootPath);
                Random ran = new Random((int)DateTime.Now.Ticks);//利用时间种子解决伪随机数短时间重复问题  
                string fileName = DateTime.Now.ToString("mmssms") + ran.Next(99999);
                string serverPath = dir + fileName + ext;

                //路径映射为绝对路径  
                string path = Server.MapPath(serverPath);
                if (t == "img")
                {
                    System.Drawing.Image img = System.Drawing.Image.FromStream(stream);//将流中的图片转换为Image图片对象  
                    img.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);//图片保存，JPEG格式图片较小  
                }
                else
                {
                    file.SaveAs(path);
                    data.Add("src", serverPath);
                    data.Add("title", fileName);
                    result["data"] = data;
                    result["code"] = 0;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                result["code"] = -1;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getUserList(int page, int limit, string keyWord)
        {
            var formProvider = new FormProvider();
            var dataO = keyWord == null ? formProvider.Select():formProvider.SelectByKeyWord(keyWord);
            var countO = dataO.Count;
            dataO = dataO.OrderByDescending(i => i.updateTime).Skip((page - 1) * limit).Take(limit).ToList();
            var res = new
            {
                code = 0,
                msg = "",
                count = countO,
                data = dataO,
            };
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getUserListByKeyWord(string keyWord)
        {
            var formProvider = new FormProvider();
            var data = formProvider.SelectByKeyWord(keyWord);
            //data = data.Skip((page - 1) * limit).Take(limit).ToList();
            return SuccessRes(data);
        }
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult UserDetail(int id)
        {
            if (Session["isLogin"] == null)
            {
                Response.Redirect("../Login/Login");

            }

            if (id == 0)
            {
                return View();
            }
            else
            {
                var formProvider = new FormProvider();
                var userInfo = formProvider.Select().FirstOrDefault(item => item.id == id);

                if (userInfo != null)
                {
                    var userInfoWithoutProfile = new
                    {
                        id = userInfo.id,
                        name = userInfo.name,
                        age = userInfo.age,
                        height = userInfo.height,
                        weight = userInfo.weight,
                        school = userInfo.school,
                        avatar = userInfo.avatar,
                        createTime = userInfo.createTime,
                        updateTime = userInfo.updateTime
                    };
                    var userInfoOnlyProfile = new
                    {
                        profile = userInfo.profile
                    };

                    ViewBag.userInfoWithoutProfile = Newtonsoft.Json.JsonConvert.SerializeObject(userInfoWithoutProfile);
                    ViewBag.userInfoOnlyProfile = Newtonsoft.Json.JsonConvert.SerializeObject(userInfoOnlyProfile);
                }
                return View();
            }
        }

        public ActionResult DeleteUser(int id)
        {
            var formProvider = new FormProvider();
            var model = formProvider.Select().FirstOrDefault(item => item.id == id);
            if (model != null)
            {
                int count = formProvider.Delete(model);
                if (count > 0)
                {
                    return SuccessRes("删除成功");
                }
                else
                {
                    return ErrorRes("删除失败");
                }
            }
            return ErrorRes("找不到该用户");


        }
    }
}