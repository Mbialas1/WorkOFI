using Microsoft.AspNetCore.Mvc;

namespace OFI.TasksService.Api.Controllers
{
    public class TasksController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
