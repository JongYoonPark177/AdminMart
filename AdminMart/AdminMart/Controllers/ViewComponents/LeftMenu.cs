using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection.Metadata.Ecma335;

namespace AdminMart.Controllers.ViewComponents
{
    public class LeftMenu : ViewComponent
    {
        public LeftMenu() { }

        public IViewComponentResult Invoke() 
        {
            return View();
        }
    }
}
