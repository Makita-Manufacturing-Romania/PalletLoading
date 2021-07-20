using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PalletLoading.Data;
using PalletLoading.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PalletLoading.Controllers
{
    public class CMRController : MainController
    {
        public CMRController(PalletLoadingContext context) : base(null, context, null)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
