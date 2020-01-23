using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eUniverzitet.Web.Helper;
using RS1_2018_02_13.Web.Helper;
using Microsoft.AspNetCore.Mvc;
using RS1_2018_02_13.Data;
using RS1_2018_02_13.Data.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace RS1_2018_02_13.Web.Controllers
{
    [Autorizacija]
    public class OznaceniDogadajiController : Controller
    {
        
        public IActionResult Index()
        {

         

            return View();
        }

     


    }
}