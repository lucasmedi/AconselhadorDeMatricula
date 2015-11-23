using System.Collections.Generic;

namespace Dominio.Aconselhador
{
    public class Turma
    {
        public int Numero { get; set; }
        public List<Periodo> Periodos { get; set; }

        public Turma()
        {
            Periodos = new List<Periodo>();
        }
    }
}