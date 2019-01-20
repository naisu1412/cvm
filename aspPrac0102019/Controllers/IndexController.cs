using aspPrac0102019.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace aspPrac0102019.Controllers
{
    public class IndexController : Controller
    {
        // GET: Index
        public ActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Authorization(aspPrac0102019.Models.Account accModel)
        {
            using (Proj011919Entities1 db = new Proj011919Entities1())
            {
                var userDetals = db.Accounts.Where(x => x.username == accModel.username && x.password == accModel.password).FirstOrDefault();
                if(userDetals == null)
                {
                    accModel.LoginError = "wrong username or password";
                    return View("Index", accModel);
                }
                else
                {
                    Session["user ID"] = userDetals.username;
                    return RedirectToAction("Dashboard", "Dashboard");
                }
            }

                return View();
        }
    }
}