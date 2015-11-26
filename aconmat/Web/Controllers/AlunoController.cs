using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Dominio.Aconselhador;
using Dominio.Persistencia;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class AlunoController : Controller
    {
        private AconmatContext ctx = new AconmatContext();

        // GET: Aluno
        public ActionResult Index()
        {
            var aluno = ctx.Alunos.ToList().FirstOrDefault(o => o.Matricula == User.Identity.Name);
            if (aluno.IsCoordenador)
                return RedirectToAction("Index", "Coordenador");

            return View(aluno);
        }

        public ActionResult SugerirMatricula()
        {
            return View(new SugerirViewModel());
        }

        [HttpPost]
        public ActionResult SugerirMatricula(SugerirViewModel viewModel)
        {
            var periodos = Periodo.IdentificaPeriodos(viewModel.Restricoes.Split(','));
            var aconselhador = new AconselhadorModel(HttpContext, User.Identity.Name, periodos);
            viewModel.Matricula = aconselhador.GetMatricula();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult SalvarSugestao(SugerirViewModel viewModel)
        {
            var aconselhador = new AconselhadorModel(HttpContext, User.Identity.Name);
            var matricula = aconselhador.GetMatricula();

            return RedirectToAction("Index", "Aluno");
        }

        
    }
}