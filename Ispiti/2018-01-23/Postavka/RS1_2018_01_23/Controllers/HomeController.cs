using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RS1_2018_01_23.EF;
using Microsoft.AspNetCore.Mvc;

namespace RS1_2018_01_23.Controllers
{
    public class HomeController : Controller
    {
        private MojContext _context;

        public HomeController(MojContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TestDB()
        {
            MojDBInitializer.Izvrsi(_context);
            return View(_context); 
        }

     
    }
}