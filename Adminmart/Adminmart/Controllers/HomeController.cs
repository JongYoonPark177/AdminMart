using System.Data;
using System.Diagnostics;
using Adminmart.Models;
using Microsoft.AspNetCore.Mvc;

namespace Adminmart.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TicketList()
        {
            string status = "In Progress";

            return View(TicketModel.GetList(status));
        }

        public IActionResult TicketChange([FromForm]TicketModel model) 
        {
            model.Update();

            return Redirect("/home/ticketlist");
        }

        public IActionResult BoardList(string search) 
        {

            return View(BoardModel.GetList(search));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}