using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineMVCApp.Controllers
{
    public class FlightNotFoundController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
