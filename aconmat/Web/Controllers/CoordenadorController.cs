using System.Linq;
using System.Web.Mvc;
using Dominio.Persistencia;

namespace Web.Controllers
{
    [Authorize]
    public class CoordenadorController : Controller
    {
        private AconmatContext ctx = new AconmatContext();

        // GET: Coordenador
        public ActionResult Index()
        {
            var aluno = ctx.Alunos.ToList().FirstOrDefault(o => o.Matricula == User.Identity.Name);
            if (!aluno.IsCoordenador)
                return RedirectToAction("Index", "Coordenador");

            return View();
        }
    }
}