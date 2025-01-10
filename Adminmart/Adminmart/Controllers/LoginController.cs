using System.Data;
using System.Diagnostics;
using System.Security.Claims;
using System.Web;
using Adminmart.Models;
using Adminmart.Models.Login;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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

        #region ȸ������
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

                if (input.Password != password2)
                {
                    throw new Exception("password�� password2�� �ٸ��ϴ�.");
                }

                input.ConvertPassword();
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

        #endregion

        #region �α���
        [HttpGet]
        public IActionResult Login(string msg)
        {
            ViewData["msg"] = msg;
            return View();
        }

        [HttpPost]
        [Route("/login/login")]
        public async Task<IActionResult> LoginProc([FromForm] UserModel input)
        {
            try
            {
                input.ConvertPassword();
                var user = input.GetLoginUser();

                //�α��� �۾�
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.User_Seq.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Name, user.User_Name));
                identity.AddClaim(new Claim(ClaimTypes.Name, user.Email));

                if(user.User_Name == "Test") 
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, "ADMIN"));
                }


                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
                {
                    IsPersistent = false, 
                    ExpiresUtc  = DateTime.UtcNow.AddHours(4),
                    AllowRefresh = true
                });

                return Redirect("/");
            }
            catch (Exception ex)
            {
                return Redirect($"/login/login?msg={HttpUtility.UrlEncode(ex.Message)}");
            }
        }

        #endregion

    }
}