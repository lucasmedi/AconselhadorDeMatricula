using System.Collections.Generic;

namespace Dominio.Modelos
{
    public class Cadeira
    {
        public int Id { get; set; }
        public string CodCred { get; set; }
        public string Nome { get; set; }
        public int Turma { get; set; }

        public virtual List<Periodo> Periodos { get; set; }
    }
}