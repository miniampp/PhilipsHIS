using Microsoft.AspNetCore.Mvc;
using PhilipsHIS.Models;

namespace PhilipsHIS.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult Login(Login login)
        //{
        //    if (login.Username == "amp" && login.Password == "amp")
        //    {
        //        return RedirectToAction("Index");
        //    }    
        //}
        public IActionResult Location()
        {
            return View();
        }

        public IActionResult Ward1()
        {
            return View();
        }


        public IActionResult Ward2()
        {
            return View();
        }

        public IActionResult Ward3()
        {
            return View();
        }
    }
}
