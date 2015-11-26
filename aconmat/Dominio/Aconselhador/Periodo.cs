using Dominio.Enums;
using System.Collections.Generic;

namespace Dominio.Aconselhador
{
    public class Periodo
    {
        public Horario Horario { get; set; }
        public DiaSemana DiaSemana { get; set; }

        public Periodo() { }
        public Periodo(Horario horario, DiaSemana diaSemana)
        {
            Horario = horario;
            DiaSemana = diaSemana;
        }


        public static List<Periodo> IdentificaPeriodos(string p)
        {
            var periodos = new List<Periodo>();

            if (p.Length == 3)
            {
                periodos.Add(IdentificaPeriodo(p));
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
                    periodos.Add(IdentificaPeriodo(str));
                }
            }

            return periodos;
        }

        public static List<Periodo> IdentificaPeriodos(string [] p)
        {
            var periodos = new List<Periodo>();

            for (int i = 0; i < p.Length; i++)
            {
                periodos.AddRange(IdentificaPeriodos(p[i]));
            }

            return periodos;
        }


        public static Periodo IdentificaPeriodo(string p)
        {
            var periodo = new Periodo();

            periodo.DiaSemana = (DiaSemana)int.Parse(p[0].ToString());

            var tn = p[1].ToString() + p[2].ToString();
            switch (tn)
            {
                case "AB":
                    periodo.Horario = Horario.AB;
                    break;
                case "CD":
                    periodo.Horario = Horario.CD;
                    break;
                case "FG":
                    periodo.Horario = Horario.FG;
                    break;
                case "HI":
                    periodo.Horario = Horario.HI;
                    break;
                case "JK":
                    periodo.Horario = Horario.JK;
                    break;
                case "LM":
                    periodo.Horario = Horario.LM;
                    break;
                case "NP":
                    periodo.Horario = Horario.NP;
                    break;
            }

            return periodo;
        }
    }
}