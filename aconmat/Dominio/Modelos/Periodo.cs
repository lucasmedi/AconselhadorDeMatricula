using Dominio.Enums;

namespace Dominio.Modelos
{
    public class Periodo
    {
        public int Id { get; set; }
        public Horario Horario { get; set; }
        public DiaSemana DiaSemana { get; set; }
    }
}