using PalletLoading.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace PalletLoading.Models
{
    public class MainController:Controller
    {
        protected readonly ILogger logger;
        protected readonly  PalletLoadingContext _context;
        protected readonly IWebHostEnvironment webHost;

        public MainController(ILogger logger, PalletLoadingContext context, IWebHostEnvironment iwhe)
        {
            this.logger = logger;
            _context = context;
            this.webHost = iwhe;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var text = Request.Headers["User-Agent"].ToString();
            var picker = "";
            try
            {
                picker = HttpContext.User.FindFirst(c => c.Type == ClaimTypes.Name).Value.Replace("MMRMAKITA\\", "");

            }
            catch { }

            if (!_context.User.Any(c => c.Username.Contains(picker)))
            {
                var actionName = filterContext.ActionDescriptor.RouteValues["action"];
                if (!actionName.Equals("AccesDenied"))
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "Home",
                        action = "AccesDenied"
                    }));
                }
            }
            else
            {
                ViewBag.FullName = this._context.User.Where(c => c.Username.Equals(User.Identity.Name.Replace("MMRMAKITA\\", ""))).Select(c => c.FullName).FirstOrDefault();
                ViewBag.Username = this._context.User.Where(c => c.Username.Equals(User.Identity.Name.Replace("MMRMAKITA\\", ""))).Select(c => c.Username).FirstOrDefault();
                ViewBag.UserRightLevel = this._context.User.Include(c => c.UserRight).Where(c => c.Username.Equals(User.Identity.Name.Replace("MMRMAKITA\\", ""))).Select(c => c.UserRight.RightLevel).FirstOrDefault();
                //var controllerName = filterContext.ActionDescriptor.RouteValues["controller"];
                //if ((controllerName.Equals("Users") || controllerName.Equals("StaffPhoneNumbers") || controllerName.Equals("UserRoles") || controllerName.Equals("UserRights")) && ViewBag.UserLevel == 2)
                //{
                //    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                //    {
                //        controller = "Home",
                //        action = "Index"
                //    }));
                //}
            }
        }

    }
}
