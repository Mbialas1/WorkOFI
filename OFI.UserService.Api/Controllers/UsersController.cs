using Microsoft.AspNetCore.Mvc;

namespace OFI.UserService.Api.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
