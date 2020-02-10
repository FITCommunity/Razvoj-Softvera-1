using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RS1_2019_12_16.EF;

namespace RS1_2019_12_16.Controllers
{
    public class AjaxStavkeController : Controller
    {
        private readonly MojContext ctx;

        public AjaxStavkeController(MojContext context)
        {
            ctx = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}