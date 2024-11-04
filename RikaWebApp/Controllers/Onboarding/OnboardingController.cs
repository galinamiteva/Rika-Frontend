using Microsoft.AspNetCore.Mvc;

namespace RikaWebApp.Controllers.Onboarding
{
    public class OnboardingController : Controller
    {
        [Route("/")]
        public IActionResult Onboarding()
        {
            return View();
        }
    }  


}
