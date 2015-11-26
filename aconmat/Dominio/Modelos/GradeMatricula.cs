using System.Collections.Generic;

namespace Dominio.Modelos
{
    public class GradeMatricula
    {
        public int Id { get; set; }
        public virtual List<Cadeira> Disciplinas { get; set; }

        public GradeMatricula()
        {
            Disciplinas = new List<Cadeira>();
        }
    }
}