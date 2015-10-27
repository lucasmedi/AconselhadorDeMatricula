using System.Collections.Generic;

namespace Dominio.Modelos
{
    public class Disciplina
    {
        private IEnumerable<Turma> Turmas;

        public string CodCred { get; set; }
        public int Creditos { get; set; }
        public int Nivel { get; set; }
        public string Nome { get; set; }

        public Disciplina()
        {
            Turmas = new List<Turma>();
        }
    }
}