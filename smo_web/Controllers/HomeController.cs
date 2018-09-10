using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using smo_data.models;
using smo_web.Models;

namespace smo_web.Controllers
{
    public class HomeController : Controller
    {
        private TrainingUKSWContext Db;
        public HomeController(TrainingUKSWContext postgresContext)
        {
            this.Db = postgresContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            var data  =(from m in Db.TrUser
                        select new UserViewModel{ Email=m.Email,UserName= m.UserName}).ToList();
            
            return View(data);
        }
        [Authorize]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    
}
