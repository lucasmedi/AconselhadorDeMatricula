namespace Dominio.Aconselhador
{
    public class Celula
    {
        public bool Bloqueado { get; set; }
        public string CodCred { get; set; }
        public int Turma { get; set; }

        public Celula()
        {
            Bloqueado = false;
        }
    }
}