using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApp.Areas.Admin.Data;
using WebApp.Areas.Admin.Models;

namespace WebApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly DataProviderContext dataProvider;
        public LoginController(DataProviderContext dataProviderContext)
        {
            dataProvider = dataProviderContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Login([Bind("UserName,Password")] Member member)
        {
            var r = dataProvider.Members.Where(m => (m.UserName == member.UserName && m.Password
             == StringProcessing.CreateMD5Hash(member.Password))).ToList();

            if(r.Count==0)
            {
                return View("Index");
            }
            var str = JsonConvert.SerializeObject(member);
            HttpContext.Session.SetString("user",str);
            // mat khau la gi?
            
            if(r[0].Role==0)
            {
                var url = Url.RouteUrl("areas", new { controller = "Home", action = "Index", area = "Admin" });
                return Redirect(url);
            }
            return  RedirectToAction("_homePartial", "Home");
        }
    }
}
