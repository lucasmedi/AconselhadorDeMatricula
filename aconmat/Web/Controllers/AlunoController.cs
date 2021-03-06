﻿using System.Collections.Generic;
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
            var usuario = ctx.Alunos.ToList().FirstOrDefault(o => o.Matricula == User.Identity.Name);
            if (usuario.IsCoordenador)
                return RedirectToAction("Index", "Coordenador");

            return View(new AlunoViewModel(usuario));
        }

        public ActionResult SugerirMatricula()
        {
            return View(new SugerirViewModel());
        }

        [HttpPost]
        public ActionResult SugerirMatricula(SugerirViewModel viewModel)
        {
            var periodos = new List<Dominio.Aconselhador.Periodo>();
            if (!string.IsNullOrEmpty(viewModel.Restricoes))
            {
                periodos = Dominio.Aconselhador.Periodo.IdentificaPeriodos(viewModel.Restricoes.Split(','));
            }

            var aconselhador = new AconselhadorModel(HttpContext, User.Identity.Name, periodos);
            viewModel.Matricula = aconselhador.GetMatricula();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult SalvarSugestao(SugerirViewModel viewModel)
        {
            var periodos = new List<Dominio.Aconselhador.Periodo>();
            if (!string.IsNullOrEmpty(viewModel.Restricoes))
            {
                periodos = Dominio.Aconselhador.Periodo.IdentificaPeriodos(viewModel.Restricoes.Split(','));
            }

            var aconselhador = new AconselhadorModel(HttpContext, User.Identity.Name, periodos);
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

        [HttpPost]
        public ActionResult ExcluirSugestao(SugerirViewModel viewModel)
        {
            var usuario = ctx.Alunos.ToList().FirstOrDefault(o => o.Matricula == User.Identity.Name);
            if (usuario != null)
            {
                using (var c = ctx.Database.BeginTransaction())
                {
                    foreach (var item in usuario.Grade.Disciplinas)
                    {
                        ctx.Periodos.RemoveRange(item.Periodos);
                    }
                    ctx.Cadeiras.RemoveRange(usuario.Grade.Disciplinas);
                    ctx.Matriculas.Remove(usuario.Grade);
                    usuario.Grade = null;
                    ctx.SaveChanges();
                    c.Commit();
                }
            }

            return RedirectToAction("Index", "Aluno");
        }
    }
}