using System.Collections.Generic;
using System.Data.Entity;
using Dominio.Modelos;

namespace Dominio.Persistencia
{
    public class AconmatInitializer : DropCreateDatabaseIfModelChanges<AconmatContext>
    {
        protected override void Seed(AconmatContext context)
        {
            var alunos = new List<Aluno>()
            {
                new Aluno { Matricula = "101004026", Nome = "Lucas da Costa Cunha" },
                new Aluno { Matricula = "131803777", Nome = "Giovanni Batista Carlos" },
                new Aluno { Matricula = "YYYYYYYYY", Nome = "Julia Freitas" },
                new Aluno { Matricula = "ZZZZZZZZZ", Nome = "Ariane Figueiredo" },
                new Aluno { Matricula = "123456789", Nome = "Dilnei Venturini", IsCoordenador = true }
            };

            context.Alunos.AddRange(alunos);
        }
    }
}