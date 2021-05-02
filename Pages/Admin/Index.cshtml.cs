using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace pizza_mama.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private IConfiguration configuration;
        public bool errorInvalidAccount;
        public bool IsDevelopmentMode;

        public IndexModel(IConfiguration configuration, IWebHostEnvironment env)
        {
            this.configuration = configuration;
            this.errorInvalidAccount = false;

            if(env.IsDevelopment())
            {
                IsDevelopmentMode = true;
            }
        }
        public IActionResult OnGet()
        {
            if(HttpContext.User.Identity.IsAuthenticated)
            {
                return Redirect("/Admin/Pizzas");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string username, string password, string ReturnUrl)
        {
            var authSection = configuration.GetSection("Auth");
            string adminLogin = authSection["AdminLogin"];
            string passwordLogin = authSection["PasswordLogin"];

            if(username == adminLogin && password == passwordLogin)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username)
                };

                var claimsIdentity = new ClaimsIdentity(claims, "Login");
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return Redirect(ReturnUrl == null ? "/Admin/Pizzas" : ReturnUrl);
            }

            this.errorInvalidAccount = true;
            
            return Page();
        }

        public async Task<IActionResult> OnGetLogout()
        {
            await HttpContext.SignOutAsync();

            return Redirect("/Admin");
        }
    }
}
