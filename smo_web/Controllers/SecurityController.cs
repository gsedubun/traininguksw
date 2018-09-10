using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using smo_data.models;
using smo_web.Models;

namespace smo_web.Controllers
{
    public class SecurityController : Controller
    {
        private TrainingUKSWContext Db;

        public SecurityController(TrainingUKSWContext  context)
        {
            this.Db = context;
        }
        public IActionResult Denied()
        {
            return View();
        }

        public IActionResult Login()
        {
            ViewData["Title"] = "Login";
            ViewData["Message"] = "Silakan isi login form di bawah ini.";
            SignOut(CookieAuthenticationDefaults.AuthenticationScheme).ExecuteResult(ControllerContext);
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel loginViewModel, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                // set claimsidentity
                var user = Db.TrUser.SingleOrDefault(d => d.UserName == loginViewModel.UserName && d.Password == loginViewModel.Password);
                if (user != null)
                {
                    var role = (from ur in Db.TtUserRole
                                join r in Db.TrRole on ur.RoleId equals r.RoleId
                                select new { Role = r.RoleName, ur.User }
                    ).ToList();

                    if (role == null)
                        return View(loginViewModel);

                    var claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, loginViewModel.UserName));
                    claims.Add(new Claim(ClaimTypes.Name, loginViewModel.UserName));
                    claims.Add(new Claim(ClaimTypes.Email, loginViewModel.UserName));
                    foreach (var r in role)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, r.Role));
                    }
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    // set authentication properties
                    var authProps = new AuthenticationProperties
                    {
                        IsPersistent = false,
                    };
                    if (!string.IsNullOrEmpty(ReturnUrl))
                        authProps.RedirectUri = ReturnUrl;
                    else
                        authProps.RedirectUri = "/Home";

                    var s = SignIn(principal, CookieAuthenticationDefaults.AuthenticationScheme);
                    s.Properties = authProps;

                    //RedirectToAction("Index","Home");
                    return s;
                    //RedirectToActionPermanent("Index","Home");
                }
                else
                {
                    //ModelState.AddModelError("InvalidLogin", new System.Exception("username and password is invalid."));
                    ModelState.AddModelError(string.Empty, "Username and Password is invalid.");
                    //ViewData["Message"]="Username and Password is invalid.";

                }
            }
            return View(loginViewModel);
        }
        public IActionResult SignOut()
        {
            var props = new AuthenticationProperties
            {
                RedirectUri = "/Home/Index"
            };
            var s = SignOut(CookieAuthenticationDefaults.AuthenticationScheme);
            s.Properties = props;
            return s;
        }
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
    }
}