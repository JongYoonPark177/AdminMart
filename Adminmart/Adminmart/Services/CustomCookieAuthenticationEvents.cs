using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Globalization;
using System.Security.Claims;

namespace Adminmart.Services
{
    public class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
    {
        public CustomCookieAuthenticationEvents() 
        {
        
        }

        public override async Task ValidatePrincipal(CookieValidatePrincipalContext context) 
        {
            var userPrincipal = context.Principal;

            var checkClaim = userPrincipal.Claims.First(p => p.Type == "LastCheckDateTime");
            var lastCheckDateTime = DateTime.ParseExact(userPrincipal.Claims.First(p => p.Type == "LastCheckDateTime").Value
                                    , "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
            var intervalMin = 15;

            if(lastCheckDateTime.AddMinutes(15) > DateTime.UtcNow)
            {
                //이 사용자가 정상 사용자인지 검증
                if (1==1)//일단 무조건 정상 
                {
                    var identity = userPrincipal.Identity as ClaimsIdentity;
                    identity.RemoveClaim(checkClaim);
                    identity.AddClaim(new Claim("LastCheckDateTime", DateTime.UtcNow.ToString("yyyyMMddHHmmss")));

                    await context.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal);
                }
                else
                {
                    context.RejectPrincipal();
                    await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                }
            }
        }
    }
}
