using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Aconselhador
{
    public class Disciplina
    {
        public string CodCred { get; set; }
        public int Creditos { get; set; }
        public int Nivel { get; set; }
        public string Nome { get; set; }
        public int Turma { get; set; }
    }
}