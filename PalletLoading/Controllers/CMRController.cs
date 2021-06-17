using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PalletLoading.Controllers
{
    public class CMRController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
