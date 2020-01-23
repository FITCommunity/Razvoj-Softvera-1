using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RS1_2019_11_04.EF;


namespace RS1_2019_11_04.Controllers
{
    public class PopravniIspit : Controller
    {

       private MojContext _db;

      public PopravniIspit(MojContext db)
        {
            _db = db;
        }


       
    }
}