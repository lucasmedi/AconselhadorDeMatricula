using System.Linq;
using System.Web.Mvc;
using Dominio.Persistencia;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class CoordenadorController : Controller
    {
        private AconmatContext ctx = new AconmatContext();

        // GET: Coordenador
        public ActionResult Index()
        {
            var usuario = ctx.Alunos.ToList().FirstOrDefault(o => o.Matricula == User.Identity.Name);
            if (!usuario.IsCoordenador)
                return RedirectToAction("Index", "Coordenador");

            var alunos = ctx.Alunos.Where(o => !o.IsCoordenador).ToList().Select(o => new AlunoViewModel(o)).ToList();

            var viewModel = new CoordenadorViewModel()
            {
                Usuario = new AlunoViewModel(usuario),
                Alunos = alunos
            };

            return View(viewModel);
        }

        public ActionResult VerMatricula(string matricula)
        {
            var usuario = ctx.Alunos.ToList().FirstOrDefault(o => o.Matricula == User.Identity.Name);
            if (!usuario.IsCoordenador)
                return RedirectToAction("Index", "Coordenador");

            var aluno = ctx.Alunos.Where(o => !o.IsCoordenador && o.Matricula == matricula).FirstOrDefault();
            if (aluno == null)
                return RedirectToAction("Index", "Coordenador");

            return View(new AlunoViewModel(aluno));
        }
    }
}