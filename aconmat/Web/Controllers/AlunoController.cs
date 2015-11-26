using System.Linq;
using System.Web.Mvc;
using Dominio.Modelos;
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

            return View(new AlunoViewModel(aluno));
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

            var aluno = ctx.Alunos.FirstOrDefault(o => o.Matricula == User.Identity.Name);
            if (aluno != null)
            {
                aluno.Grade = new GradeMatricula();

                var disciplinas = matricula.GetDisciplinas();
                foreach (var disciplina in disciplinas)
                {
                    var cadeira = new Cadeira
                    {
                        CodCred = disciplina.CodCred,
                        Nome = disciplina.Nome
                    };

                    if (disciplina.TurmaSelecionada != null)
                    {
                        cadeira.Turma = disciplina.TurmaSelecionada.Numero;
                        cadeira.Periodos = disciplina.TurmaSelecionada.Periodos.Select(o => new Periodo()
                        {
                            DiaSemana = o.DiaSemana,
                            Horario = o.Horario
                        }).ToList();
                    }

                    aluno.Grade.Disciplinas.Add(cadeira);
                }

                ctx.SaveChanges();
            }

            return RedirectToAction("Index", "Aluno");
        }

        
    }
}