using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dominio.Aconselhador;
using Dominio.Enums;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var disciplinasPendentes = new List<Disciplina>();

            var reader = new StreamReader(@"C:\Projetos\GitHub\AconselhadorDeMatricula\aconmat\ConsoleApp\files\disciplinasPendentes.csv");

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                var columns = line.Split(';');

                var disciplina = new Disciplina();
                disciplina.Nivel = int.Parse(columns[0]);
                var cod = columns[1].Split('-');
                disciplina.CodCred = cod[0];
                disciplina.Creditos = int.Parse(cod[1]);
                disciplina.Nome = columns[2];
                disciplina.CargaHoraria = int.Parse(columns[3]);

                if (!string.IsNullOrEmpty(columns[4]))
                {
                    var dep = columns[4].Split(',');
                    for (int i = 0; i < dep.Length; i++)
                    {
                        var discRequisito = disciplinasPendentes.FirstOrDefault(o => o.CodCred == dep[i]);
                        if (discRequisito != null)
                        {
                            disciplina.Prerequisitos.Add(discRequisito);
                        }
                    }

                }

                if (!string.IsNullOrEmpty(columns[5]))
                {
                    var turmas = columns[5].Split(',');

                    for (int i = 0; i < turmas.Length; i++)
                    {
                        var t = turmas[i].Split('-');
                        var turma = new Turma();
                        turma.Numero = int.Parse(t[0]);
                        turma.Periodos = identificaPeriodos(t[1]);

                        disciplina.Turmas.Add(turma);
                    }
                }

                disciplinasPendentes.Add(disciplina);
            }

            var aconselhador = new Aconselhador(disciplinasPendentes, null);

            aconselhador.GetMatricula();

            Console.ReadKey();
        }

        private static List<Periodo> identificaPeriodos(string p)
        {
            var periodos = new List<Periodo>();

            if (p.Length == 3)
            {
                periodos.Add(identificaPeriodo(p));
            }

            if (p.Length == 5)
            {
                p = p.Insert(3, p[0].ToString());
            }

            if (p.Length >= 6)
            {
                for (int i = 0; i < p.Length; i += 3)
                {
                    var str = p[i].ToString() + p[i + 1].ToString() + p[i + 2].ToString();
                    periodos.Add(identificaPeriodo(str));
                }
            }

            return periodos;
        }

        private static Periodo identificaPeriodo(string p)
        {
            var periodo = new Periodo();

            periodo.DiaSemana = (EnumDiaSemana)int.Parse(p[0].ToString());

            var tn = p[1].ToString() + p[2].ToString();
            switch (tn)
            {
                case "AB":
                    periodo.Horario = EnumHorario.AB;
                    break;
                case "CD":
                    periodo.Horario = EnumHorario.CD;
                    break;
                case "FG":
                    periodo.Horario = EnumHorario.FG;
                    break;
                case "HI":
                    periodo.Horario = EnumHorario.HI;
                    break;
                case "JK":
                    periodo.Horario = EnumHorario.JK;
                    break;
                case "LM":
                    periodo.Horario = EnumHorario.LM;
                    break;
                case "NP":
                    periodo.Horario = EnumHorario.NP;
                    break;
            }

            return periodo;
        }
    }
}