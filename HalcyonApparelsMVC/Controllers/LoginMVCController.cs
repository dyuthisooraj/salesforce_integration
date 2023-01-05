using Microsoft.AspNetCore.Mvc;
using HalcyonApparelsMVC.Models;

namespace HalcyonApparelsMVC.Controllers
{
    public class LoginMVCController : Controller
    {

        public IActionResult Login()
        {
            //HttpContext.Session.GetString("Acces_token");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginMVC loginDetails)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7200");
            var postTask = client.PostAsJsonAsync("api/Login", loginDetails);
            postTask.Wait();
            var Result = postTask.Result;
            if (!Result.IsSuccessStatusCode)
            {
                ViewData["LoginFlag"] = "Invalid Login";
                return View();
            }
            return RedirectToAction("AccessoryView", "Home");
        }


    }

    
}
