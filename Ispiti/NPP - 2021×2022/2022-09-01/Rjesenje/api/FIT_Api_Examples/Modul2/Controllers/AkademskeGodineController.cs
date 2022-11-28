using System;
using System.Collections.Generic;
using System.Linq;
using FIT_Api_Examples.Data;
using FIT_Api_Examples.Helper;
using FIT_Api_Examples.Helper.AutentifikacijaAutorizacija;
using FIT_Api_Examples.Modul3.Models;
using FIT_Api_Examples.Modul4_MaticnaKnjiga.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FIT_Api_Examples.Modul4_MaticnaKnjiga.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class AkademskeGodineController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public AkademskeGodineController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public List<CmbStavke> GetAll_ForCmb()
        {
            return _dbContext.AkademskaGodina
                .OrderByDescending(x => x.id)
                .Select(s=>new CmbStavke
                {
                    opis = s.opis,
                    id = s.id
                })
                .ToList();
        }
    }
}
