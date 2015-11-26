using System.Collections.Generic;

namespace Web.Models
{
    public class CoordenadorViewModel
    {
        public AlunoViewModel Usuario { get; set; }
        public List<AlunoViewModel> Alunos { get; set; }
    }
}