using System.Collections.Generic;
using System.Data.Entity;
using Dominio.Modelos;

namespace Dominio.Persistencia
{
    public class AconmatInitializer : DropCreateDatabaseAlways<AconmatContext>
    {
        protected override void Seed(AconmatContext context)
        {
            var alunos = new List<Aluno>()
            {
                new Aluno { Matricula = "101004026", Nome = "Lucas da Costa Cunha" },
                new Aluno { Matricula = "131803777", Nome = "Giovanni Batista Carlos" },
                new Aluno { Matricula = "082001777", Nome = "Julia Freitas" },
                new Aluno { Matricula = "091003087", Nome = "Ariane Figueiredo" },
                new Aluno { Matricula = "123456789", Nome = "Dilnei Venturini", IsCoordenador = true }
            };

            context.Alunos.AddRange(alunos);
        }
    }
}