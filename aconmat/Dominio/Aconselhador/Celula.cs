namespace Dominio.Aconselhador
{
    public class Celula
    {
        public bool Livre { get; set; }
        public string CodCred { get; set; }
        public int Turma { get; set; }

        public Celula()
        {
            Livre = true;
        }
    }
}