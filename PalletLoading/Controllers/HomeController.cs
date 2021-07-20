using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PalletLoading.Data;
using PalletLoading.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PalletLoading.Controllers
{
    public class HomeController : MainController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, PalletLoadingContext context):base(logger,context,null)
        {
        }
        public IActionResult AccesDenied()
        {

            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
