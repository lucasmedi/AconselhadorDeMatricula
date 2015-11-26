using System.Collections.Generic;
using Dominio.Modelos;

namespace Web.Models
{
    public class GradeViewModel
    {
        public CelulaViewModel[,] _grade;
        private List<Cadeira> _cadeiras;

        public GradeViewModel(GradeMatricula matricula)
        {
            _grade = new CelulaViewModel[6, 7];
            _cadeiras = new List<Cadeira>();

            foreach (var cadeira in matricula.Disciplinas)
            {
                if (cadeira.Turma == 0)
                {
                    _cadeiras.Add(cadeira);
                    continue;
                }

                foreach (var periodo in cadeira.Periodos)
                {
                    _grade[(int)periodo.DiaSemana - 2, (int)periodo.Horario - 1] = new CelulaViewModel()
                    {
                        Bloqueado = false,
                        CodCred = cadeira.CodCred,
                        Turma = cadeira.Turma
                    };
                }

                _cadeiras.Add(cadeira);
            }
        }

        public CelulaViewModel[,] GetGrade()
        {
            return _grade;
        }

        public List<Cadeira> GetCadeiras()
        {
            return _cadeiras;
        }
    }
}