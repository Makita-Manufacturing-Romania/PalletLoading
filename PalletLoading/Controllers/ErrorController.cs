using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PalletLoading.Data;
using PalletLoading.Models;
using System.Linq;

namespace PalletLoading.Controllers
{
    public class ErrorController : MainController
    {
        public ErrorController(PalletLoadingContext context) : base(null, context, null)
        {
        }

        public IActionResult Index(int statusCode)
        {
            string errorMessage = "";

            switch (statusCode)
            {
                case 403:
                    errorMessage = "Nivelul dumneavoastra de acces este retrictionat pentru aceasta pagina!!";
                    break;
                case 404:
                    errorMessage = "Pagina nu a fost gasita!";
                    break;
                default:
                    errorMessage = "Eroare Aplicatie!";
                    break;
            }

            ViewBag.ErrorCode = "Error " + statusCode;
            ViewBag.ErrorMessage = errorMessage;
            return View();
        }

    }
}
