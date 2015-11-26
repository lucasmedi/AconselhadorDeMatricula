using System.Collections.Generic;
using System.Diagnostics;

namespace Dominio.Aconselhador
{
    [DebuggerDisplay("{CodCred}-{Nome}")]
    public class Disciplina
    {
        public string CodCred { get; set; }
        public string Nome { get; set; }
        public int Nivel { get; set; }
        public int Creditos { get; set; }
        public int CargaHoraria { get; set; }

        public bool Especial { get; set; }

        public int? MinimoCreditosCursados { get; set; }

        public List<Disciplina> Prerequisitos { get; set; }
        public List<Turma> Turmas { get; set; }

        public Turma TurmaSelecionada { get; set; }

        public Disciplina()
        {
            Prerequisitos = new List<Disciplina>();
            Turmas = new List<Turma>();
        }
    }
}