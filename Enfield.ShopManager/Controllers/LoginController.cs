using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Enfield.ShopManager.Models;
using Enfield.ShopManager.Security;
using Enfield.ShopManager.Helpers;

namespace Enfield.ShopManager.Controllers
{
    [AllowAnonymous]
    public class LoginController : RestrictedControllerBase
    {
        //TODO: exclude from token validation
        public ActionResult Index(string returnUrl)
        {
            var model = new LoginModel() { ReturnUrl = returnUrl };
            ViewBag.Locations = LocationServices.GetLocationLookup();
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ValidateLogin(LoginModel login)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = SecurityServices.ValidateUser(login.Name, login.Password);
                    if (user == null)
                    {
                        SecurityServices.RecordFailedLoginAttempt(login, HttpContext.Request.UserHostAddress, string.Format("Invalid credentials: {0} - {1}", login.Name, login.Password));
                        ModelState.AddModelError(String.Empty, "The login name or password is invalid.");
                    }
                    if (user != null && !user.CanLogin)
                    {
                        SecurityServices.RecordFailedLoginAttempt(login, HttpContext.Request.UserHostAddress, "User login disabled");
                        ModelState.AddModelError(String.Empty, "Access denied.");
                    }
                    if (user != null && !IsIpValid(user.RoleName, login.LocationId))
                    {
                        SecurityServices.RecordFailedLoginAttempt(login, HttpContext.Request.UserHostAddress, "Invalid IP address");
                        ModelState.AddModelError(String.Empty, "Access denied.");
                    }

                    if (ModelState.IsValid) 
                    {
                        if (login.DowngradeRole)
                        {
                            user.RoleName = "Employee";
                            SecurityServices.RecordSuccessfulLoginAttempt(login, HttpContext.Request.UserHostAddress, "Downgraded to Employee role");
                        }
                        else if (user.RoleName == "Manager" && login.LocationId != user.LocationId)
                        {
                            user.RoleName = "Employee";
                            var message = string.Format("Manager downgraded to Employee role");
                            SecurityServices.RecordSuccessfulLoginAttempt(login, HttpContext.Request.UserHostAddress, message);
                        }
                        else
                        {
                            SecurityServices.RecordSuccessfulLoginAttempt(login, HttpContext.Request.UserHostAddress);
                        }

                        var token = CreateToken(user.Id, user.RoleName, login.LocationId);
                        var auth = TokenSerializer.GetCookieFromToken(token);
                        if (HttpContext.Request.IsLocal) //local development overrides
                        {
                            auth.Domain = null;
                            auth.Secure = false;
                        }
                        HttpContext.Response.Cookies.Add(auth);

                        if (Url.IsLocalUrl(login.ReturnUrl) && user.RoleName == "Administrator")
                            return Redirect(login.ReturnUrl);
                        else
                            return RedirectToAction("Index", "ShopFloor");
                    }
                }
                else
                {
                    SecurityServices.RecordFailedLoginAttempt(login, HttpContext.Request.UserHostAddress, string.Format("Invalid model state: {0}", ModelState.ToString()));
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                ModelState.AddModelError(String.Empty, Constants.ServerError);
            }

            // Invalid - redisplay with errors
            ViewBag.Locations = LocationServices.GetLocationLookup();
            return View("Index", login);
        }

        public ActionResult LogOff()
        {
            var token = CreateToken(0, "Employee", 0);
            token.CreateDate = DateTime.Today.AddYears(-1);
            var auth = TokenSerializer.GetCookieFromToken(token);
            if (HttpContext.Request.IsLocal) //local development overrides
            {
                auth.Domain = null;
                auth.Secure = false;
            }
            HttpContext.Response.Cookies.Add(auth);

            return RedirectToAction("Index");
        }

        private bool IsIpValid(string role, int locationId)
        {
            var location = LocationServices.GetLocation(locationId);
            if (!string.IsNullOrEmpty(location.StaticIpAddress) &&
                role != "Administrator" &&
                role != "Manager" &&
                !HttpContext.Request.IsLocal &&
                location.StaticIpAddress != HttpContext.Request.UserHostAddress)
            {
                return false;
            }
            return true;
        }

        private Token CreateToken(int userId, string role, int locationId)
        {
            var token = new Token()
            {
                CreateDate = DateTime.Now,
                IpAddress = HttpContext.Request.UserHostAddress,
                LocationId = locationId,
                Role = (int)Enum.Parse(typeof(RolesEnum), role),
                UserId = userId
            };
            TokenHasher.Hash(token);

            return token;
        }
    }
}