using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bussiness.Interface;
using EnjoyShare.Framework.Log;

namespace EnjoyShare.WebSite.Controllers
{
    public class HomeController : Controller
    {
        #region Identity
        //private ISearch iCommoditySearch = null;
        private IUserAccountService _iBaseService = null;
        //private ICategoryService iCategoryService = null;
        private int pageSize = 10;
        private Logger logger = Logger.CreateLogger(typeof(HomeController));

        public HomeController(IUserAccountService baseService)
        {
            this._iBaseService = baseService;
        }
        #endregion Identity
        public ActionResult Index()
        {
            return View();
        }
    }
}