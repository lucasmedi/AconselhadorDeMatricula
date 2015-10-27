using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Aconselhador
{
    public class Grade
    {
        private Disciplina[,] grade;

        public Grade()
        {
            grade = new Disciplina[6, 5];
        }

        public Grade(List<Periodo> bloqueados)
        {
            grade = new Disciplina[6, 5];
        }
    }
}