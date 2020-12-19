using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using WebApp.Areas.Admin.Models;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            JObject us = JObject.Parse(HttpContext.Session.GetString("user"));
            Member member = new Member();
            member.UserName = us.SelectToken("UserName").ToString();
            member.Password = us.SelectToken("Password").ToString();
            member.Role = Int32.Parse(us.SelectToken("Role").ToString());
            return View(member);
        }
    }
}
