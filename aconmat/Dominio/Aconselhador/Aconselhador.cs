using System.Collections.Generic;

namespace Dominio.Aconselhador
{
    public class Aconselhador
    {
        private IList<Disciplina> _disciplinas;

        public Aconselhador(IList<Disciplina> disciplinas)
        {
            _disciplinas = disciplinas;
        }

        public Matricula GetMatricula()
        {
            return new Matricula();
        }
    }
}