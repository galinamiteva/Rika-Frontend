using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RikaWebApp.Models.Auth;
using System.Text;

namespace RikaWebApp.Controllers.Auth
{
    public class ValidateController : Controller
    {

        private readonly HttpClient _httpClient;

        public ValidateController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public IActionResult ValidateView()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Validate(ValidateModel model)
        {
            if (ModelState.IsValid)
            {
                using HttpClient http = new HttpClient();
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var response = await http.PostAsync("https://authproviderrika.azurewebsites.net/api/verification/verify", content);

                switch (response.StatusCode)
                {
                    

                    case System.Net.HttpStatusCode.BadRequest:
                        TempData["ErrorRegister"] = "Invalid form data";
                        break;

                    case System.Net.HttpStatusCode.InternalServerError:
                        TempData["ErrorRegister"] = "Internal Server Error";
                        break;

                    case System.Net.HttpStatusCode.OK:
                        return RedirectToAction("SignInView", "SignIn");
                }

                return View("ValidateView", model);
            }

            return View("ValidateView", model);
        }


    }
}
