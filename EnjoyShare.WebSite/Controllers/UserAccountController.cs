using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EnjoyShare.Framework.Extension;
using EnjoyShare.Framework.ImageHelper;
using EnjoyShare.WebSite.Untility;

namespace EnjoyShare.WebSite.Controllers
{
    public class UserAccountController : Controller
    {
        [HttpGet, AllowAnonymous]
        //[AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ViewResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(string name, string password, string verify)
        {
            UserManage.LoginResult result = this.HttpContext.UserLogin(name, password, verify);

            if (result == UserManage.LoginResult.Success)
            {
                if (this.HttpContext.Session["CurrentUrl"] == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    string url = this.HttpContext.Session["CurrentUrl"].ToString();
                    this.HttpContext.Session["CurrentUrl"] = null;
                    return Redirect(url);
                }
            }
            else
            {
                ModelState.AddModelError("failed", result.GetRemark());
                return View();
            }
        }
        [HttpGet, AllowAnonymous]
        public ViewResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(string name, string password, string email)
        {
            return View();
        }

        /// <summary>
        /// 验证码  直接写入Response
        /// </summary>
        [AllowAnonymous]
        public void Verify()
        {
            string code = "";
            Bitmap bitmap = VerifyCodeHelper.CreateVerifyCode(out code);
            base.HttpContext.Session["CheckCode"] = code;
            bitmap.Save(base.Response.OutputStream, ImageFormat.Gif);
            base.Response.ContentType = "image/gif";
        }


        public ActionResult Personalcenter()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Personalcenter(string name, string password, string email)
        {
            return View();
        }
        /// <summary>
        /// 左导航来
        /// </summary>
        /// <returns></returns>
        public PartialViewResult NavigationList()
        {

            return PartialView();
        }

        public PartialViewResult Address()
        {
            return PartialView();
        }
    }
}