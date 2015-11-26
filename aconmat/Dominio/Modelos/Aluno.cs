namespace Dominio.Modelos
{
    public class Aluno
    {
        public int Id { get; set; }
        public string Matricula { get; set; }
        public string Nome { get; set; }

        public bool IsCoordenador { get; set; }
    }
}