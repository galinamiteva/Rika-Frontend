using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RikaWebApp.Models.Auth;
using System.Text;

namespace RikaWebApp.Controllers.Auth;

public class SignInController : Controller
{
    private readonly HttpClient _httpClient;

    public SignInController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpGet]
    public IActionResult SignInView()
    {
        return View();
    }

    [Route("/SignIn/SignInView")]
    [HttpPost]
    public async Task<IActionResult> SignIn(SignInModel model)
    {
        if (ModelState.IsValid)
        {
            using HttpClient http = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await http.PostAsync("https://authproviderrika.azurewebsites.net/api/auth/signin", content);

            switch (response.StatusCode)
            {
                

                case System.Net.HttpStatusCode.BadRequest:
                    TempData["ErrorRegister"] = "Invalid form data";
                    break;

                case System.Net.HttpStatusCode.InternalServerError:
                    TempData["ErrorRegister"] = "Internal Server Error";
                    break;

                case System.Net.HttpStatusCode.OK:
                    return RedirectToAction("SuccessView", "Success");


            }

            return View("SignInView", model);
        }

        return View("SignInView", model);
    }
}
