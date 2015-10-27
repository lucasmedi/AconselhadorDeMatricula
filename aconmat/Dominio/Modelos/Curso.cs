using System.Collections.Generic;

namespace Dominio.Modelos
{
    public class Curso
    {
        private IEnumerable<Disciplina> Disciplinas;

        public int Codigo { get; set; }
        public string Nome { get; set; }

        public Curso()
        {
            Disciplinas = new List<Disciplina>();
        }
    }
}