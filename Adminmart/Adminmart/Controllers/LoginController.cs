using System.Data;
using System.Diagnostics;
using System.Web;
using Adminmart.Models;
using Adminmart.Models.Login;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace Adminmart.Controllers
{
    public class LoginController : Controller
    {

        public LoginController()
        {
        }

        public IActionResult Index()
        {
            return Redirect("/login/login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [Route("/login/login")]
        public IActionResult LoginProc()
        {
            return null;
        }

        public IActionResult Register(string msg)
        {
            ViewData["msg"] = msg;
            return View();
        }

        [HttpPost]
        [Route("/login/register")]
        public IActionResult RegisterProc([FromForm] UserModel input)
        {
            try
            {
                string password2 = Request.Form["password2"];
                
                if(input.Password != password2) 
                {
                    throw new Exception("password�� password2�� �ٸ��ϴ�.");
                }

                input.Register();

                //�α���

                //����
                return Redirect("/login/login");
            }
            catch (Exception ex)
            {
                //����
                return Redirect($"/login/register?msg={HttpUtility.UrlEncode(ex.Message)}");
            }
        }
    }
}