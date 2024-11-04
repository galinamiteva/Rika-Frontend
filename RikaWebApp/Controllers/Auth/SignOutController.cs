using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RikaWebApp.Models.Auth;
using System.Text;

namespace RikaWebApp.Controllers.Auth
{
    public class SignOutController : Controller
    {

        private readonly HttpClient _httpClient;

        public SignOutController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [Route("/SignOut/SignOutView")]
        [HttpGet]
        public IActionResult SignOutView()
        {
            return View();
        }

        public async Task<IActionResult> Login(ValidateModel model)
        {
            if (ModelState.IsValid)
            {
                using HttpClient http = new HttpClient();
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var response = await http.PostAsync("https://authproviderrika.azurewebsites.net/api/auth/sigout", content);

                switch (response.StatusCode)
                {


                    case System.Net.HttpStatusCode.BadRequest:
                        TempData["ErrorRegister"] = "Invalid form data";
                        break;

                    case System.Net.HttpStatusCode.InternalServerError:
                        TempData["ErrorRegister"] = "Internal Server Error";
                        break;

                    case System.Net.HttpStatusCode.Created:
                        return RedirectToAction("Index", "Home");
                }

                return View("SignOutView", model);
            }

            return View("SignOutView", model);
        }
    }
}
