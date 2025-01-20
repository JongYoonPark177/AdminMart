using System.Data;
using System.Diagnostics;
using System.Security.Claims;
using System.Web;
using Adminmart.Models;
using Adminmart.Models.Login;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace Adminmart.Controllers
{
    [Authorize]
    public class AdmimController : Controller
    {

        public AdmimController()
        {
        }
        [Authorize(Roles = "ADMIN")]

        public IActionResult GetCheck() 
        {
            return Json(new { a = 9 });
            //return Json(new { a = 1});
        }

        [AllowAnonymous]
        public IActionResult GetUserCheck() 
        {
            if (User.Identity.IsAuthenticated) 
            {
                return Json(new { a = 9 });
            }

            return Json(new { a = 1 });
        }

    }
}