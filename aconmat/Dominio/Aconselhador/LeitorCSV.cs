using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dominio.Enums;

namespace Dominio.Aconselhador
{
    public class LeitorCSV
    {
        private string _filePath;

        public LeitorCSV(string filePath)
        {
            _filePath = filePath;
        }

        public List<Disciplina> CarregaDisciplinasPendentes()
        {
            var disciplinasPendentes = new List<Disciplina>();

            var reader = new StreamReader(_filePath);

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                if (line.StartsWith("#"))
                {
                    continue;
                }

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
                    var dependencias = columns[4].Split(',');
                    for (int i = 0; i < dependencias.Length; i++)
                    {
                        if (dependencias[i].StartsWith("CRED"))
                        {
                            disciplina.MinimoCreditosCursados = int.Parse(dependencias[i].Remove(0, 4));
                        }

                        var discRequisito = disciplinasPendentes.FirstOrDefault(o => o.CodCred == dependencias[i]);
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
                        turma.Periodos = IdentificaPeriodos(t[1]);

                        disciplina.Turmas.Add(turma);
                    }
                }

                disciplinasPendentes.Add(disciplina);
            }

            return disciplinasPendentes;
        }

        private List<Periodo> IdentificaPeriodos(string p)
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

        private Periodo IdentificaPeriodo(string p)
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