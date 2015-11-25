using System.Collections.Generic;
using System.Linq;

namespace Dominio.Aconselhador
{
    public class Matricula
    {
        private Celula[,] grade;
        private List<Disciplina> normais;
        private List<Disciplina> especiais;

        private int qtdCreditos;

        public Matricula()
        {
            qtdCreditos = 0;
            grade = new Celula[6, 7];
            normais = new List<Disciplina>();
            especiais = new List<Disciplina>();
        }

        public Matricula(List<Periodo> bloqueados)
        {
            qtdCreditos = 0;
            grade = new Celula[6, 7];
            normais = new List<Disciplina>();
            especiais = new List<Disciplina>();
        }

        public bool AdicionarDisciplina(Disciplina disciplina)
        {
            if (disciplina == null)
                return false;

            if (!disciplina.Turmas.Any())
            {
                qtdCreditos += disciplina.Creditos;
                especiais.Add(disciplina);
                return false;
            }

            var periodosDisponiveis = true;
            var adicionouPeriodo = false;
            foreach (var turma in disciplina.Turmas)
            {
                foreach (var periodo in turma.Periodos)
                {
                    if (grade[(int)periodo.DiaSemana - 2, (int)periodo.Horario - 1] != null)
                    {
                        periodosDisponiveis = false;
                    }
                }

                if (!periodosDisponiveis)
                {
                    periodosDisponiveis = true;
                    continue;
                }

                foreach (var periodo in turma.Periodos)
                {
                    adicionouPeriodo = true;
                    grade[(int)periodo.DiaSemana - 2, (int)periodo.Horario - 1] = new Celula()
                    {
                        CodCred = disciplina.CodCred,
                        Turma = turma.Numero
                    };
                }

                break;
            }

            if (!adicionouPeriodo)
            {
                return false;
            }

            qtdCreditos += disciplina.Creditos;
            normais.Add(disciplina);

            return true;
        }

        public Celula[,] GetGrade()
        {
            return grade;
        }

        public IList<Disciplina> GetDisciplinas()
        {
            var todas = new List<Disciplina>();
            todas.AddRange(normais);
            todas.AddRange(especiais);
            return todas;
        }

        private Disciplina BuscaDisciplina(string codcred)
        {
            var d = normais.Where(o => o.CodCred == codcred).FirstOrDefault();

            if (d == null)
                d = especiais.Where(o => o.CodCred == codcred).FirstOrDefault();

            return d;
        }
    }
}