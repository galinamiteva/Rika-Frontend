using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RikaWebApp.Models;
using RikaWebApp.Models.Auth;
using RikaWebApp.ViewModels;
using System.Reflection;
using System.Text;

namespace RikaWebApp.Controllers
{
    public class SignUpController : Controller
    {
        private readonly HttpClient _httpClient;

        public SignUpController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [Route("/SignUp/SignUpView")]
        [HttpGet]
        public IActionResult SignUpView()
        {
            return View(new SignUpModel());
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpModel model)
        {
            if (ModelState.IsValid)
            {
                using HttpClient http = new HttpClient();
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var response = await http.PostAsync("https://authproviderrika.azurewebsites.net/api/auth/signup", content);

                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.Conflict:
                        TempData["ErrorRegister"] = "Email already exists";
                        break;

                    case System.Net.HttpStatusCode.BadRequest:
                        TempData["ErrorRegister"] = "Invalid form data";
                        break;

                    case System.Net.HttpStatusCode.InternalServerError:
                        TempData["ErrorRegister"] = "Internal Server Error";
                        break;

                    case System.Net.HttpStatusCode.OK:
                        return RedirectToAction("ValidateView", "Validate");
                }

                return View("SignUpView", model);
            }

            return View("SignUpView", model);
        }
    }
}
