using System;
using System.Text;
using Dominio.Aconselhador;
using Dominio.Enums;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //var filePath = @"C:\Projetos\GitHub\AconselhadorDeMatricula\aconmat\ConsoleApp\files\disciplinasPendentes.csv";
            var filePath = @"C:\Users\lucas-cunha\Desktop\Meus Documentos\Projetos GitHub\AconselhadorDeMatricula\aconmat\ConsoleApp\files\disciplinasPendentes2.csv";

            var leitor = new LeitorCSV(filePath);
            var creditosCursados = 0;
            var disciplinasPendentes = leitor.CarregaDisciplinasPendentes(out creditosCursados);

            var restricoes = new List<Periodo>();
            restricoes.Add(new Periodo(Horario.LM, DiaSemana.Segunda));
            restricoes.Add(new Periodo(Horario.LM, DiaSemana.Terca));
            restricoes.Add(new Periodo(Horario.LM, DiaSemana.Quarta));
            restricoes.Add(new Periodo(Horario.LM, DiaSemana.Quinta));

            var aconselhador = new Aconselhador(disciplinasPendentes, restricoes, creditosCursados);
            var matricula = aconselhador.GetMatricula();
            var grade = matricula.GetGrade();

            for (int i = 0; i < grade.GetLength(1); i++)
            {
                var str = new StringBuilder();
                for (int j = 0; j < grade.GetLength(0); j++)
                {
                    var val = grade.GetValue(j, i) as Celula;
                    if (val != null && !val.Bloqueado)
                    {
                        str.Append(val.CodCred + " | ");
                    }
                }

                if (str.Length > 0)
                {
                    Console.WriteLine(EnumHelper.GetEnumDescription((Horario)i + 1) + " | " + str.ToString());
                }
            }

            Console.WriteLine();

            foreach (var item in matricula.GetDisciplinas())
            {
                Console.WriteLine(string.Format("{0}-{1:00} - {2}", item.CodCred, item.Creditos, item.Nome));
            }

            Console.ReadKey();
        }
    }
}