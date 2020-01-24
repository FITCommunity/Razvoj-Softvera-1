using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RS1_2019_12_02.EF;


namespace RS1_2019_12_02.Controllers
{
    public class LoginController : Controller
    {
        private MojContext _db;

        public LoginController(MojContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            return View();
        }

      
    }
}