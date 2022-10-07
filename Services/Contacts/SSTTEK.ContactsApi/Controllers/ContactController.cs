using Microsoft.AspNetCore.Mvc;

namespace SSTTEK.ContactsApi.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
