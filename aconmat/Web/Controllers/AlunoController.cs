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
            var filePath = HttpContext.Server.MapPath(string.Format("~/App_Data/{0}.csv", User.Identity.Name));

            var restricoes = new List<Periodo>();

            var leitor = new LeitorCSV(filePath);
            var creditosCursados = 0;
            var disciplinasPendentes = leitor.CarregaDisciplinasPendentes(out creditosCursados);
            var aconselhador = new Aconselhador(disciplinasPendentes, restricoes, creditosCursados);

            viewModel.Matricula = aconselhador.GetMatricula();

            return View(viewModel);
        }
    }
}