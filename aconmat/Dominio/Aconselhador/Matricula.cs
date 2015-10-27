using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Aconselhador
{
    public class Matricula
    {
        private Grade _grade;

        public Matricula()
        {

        }

        public Grade GetMatricula()
        {
            return _grade;
        }
    }
}