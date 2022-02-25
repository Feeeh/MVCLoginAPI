using Microsoft.AspNetCore.Mvc;
using MVCLoginAPI2.Models;
using System.Threading.Tasks;

namespace MVCLoginAPI2.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult<Usuario>> Index()
        {
            return View();
        }

        public;
    }
}
