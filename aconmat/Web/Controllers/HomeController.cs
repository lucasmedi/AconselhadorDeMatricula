using System.Web.Mvc;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Sobre a disciplina e o trabalho.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contatos";

            return View();
        }
    }
}