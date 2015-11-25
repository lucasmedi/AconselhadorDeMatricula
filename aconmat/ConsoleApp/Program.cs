using System;
using System.Text;
using Dominio.Aconselhador;
using Dominio.Enums;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var filePath = @"C:\Projetos\GitHub\AconselhadorDeMatricula\aconmat\ConsoleApp\files\disciplinasPendentes.csv";

            var leitor = new LeitorCSV(filePath);
            var disciplinasPendentes = leitor.CarregaDisciplinasPendentes();

            var aconselhador = new Aconselhador(disciplinasPendentes, null, 0);
            var matricula = aconselhador.GetMatricula();
            var grade = matricula.GetGrade();

            for (int i = 0; i < grade.GetLength(1); i++)
            {
                var str = new StringBuilder();
                for (int j = 0; j < grade.GetLength(0); j++)
                {
                    var val = grade.GetValue(j, i) as Celula;
                    if (val != null && !val.Livre)
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