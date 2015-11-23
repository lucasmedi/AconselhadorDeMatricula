using System.Collections.Generic;
using System.Linq;

namespace Dominio.Aconselhador
{
    public class Grade
    {
        private Disciplina[,] grade;
        private List<Disciplina> normais;
        private List<Disciplina> especiais;

        public Grade()
        {
            grade = new Disciplina[7, 6];
            normais = new List<Disciplina>();
            especiais = new List<Disciplina>();
        }

        public Grade(List<Periodo> bloqueados)
        {
            grade = new Disciplina[7, 6];
            normais = new List<Disciplina>();
            especiais = new List<Disciplina>();
        }

        public void AdicionarDisciplina(Disciplina disciplina)
        {
            if (disciplina == null)
                return;

            //if (!disciplina.Periodos.Any())
            //{
            //    especiais.Add(disciplina);
            //    return;
            //}

            normais.Add(disciplina);
        }

        public Disciplina BuscaDisciplina(string codcred)
        {
            var d = normais.Where(o => o.CodCred == codcred).FirstOrDefault();

            if (d == null)
                d = especiais.Where(o => o.CodCred == codcred).FirstOrDefault();

            return d;
        }
    }
}