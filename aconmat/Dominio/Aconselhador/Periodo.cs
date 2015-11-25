using Dominio.Enums;

namespace Dominio.Aconselhador
{
    public class Periodo
    {
        public Horario Horario { get; set; }
        public DiaSemana DiaSemana { get; set; }

        public Periodo() { }
        public Periodo(Horario horario, DiaSemana diaSemana)
        {
            Horario = horario;
            DiaSemana = diaSemana;
        }
    }
}