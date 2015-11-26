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

        public List<Disciplina> CarregaDisciplinasPendentes(out int creditosCursados)
        {
            var disciplinasPendentes = new List<Disciplina>();
            creditosCursados = 0;

            var reader = new StreamReader(_filePath);

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                if (line.StartsWith("#"))
                {
                    continue;
                }

                if (line.StartsWith("C"))
                {
                    var disc = line.Split(';');
                    var ind = disc[1].Split('-');
                    creditosCursados += int.Parse(ind[1]);
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
                        turma.Periodos = Periodo.IdentificaPeriodos(t[1]);

                        disciplina.Turmas.Add(turma);
                    }
                }

                if (!disciplina.Turmas.Any())
                {
                    disciplina.Especial = true;
                }

                disciplinasPendentes.Add(disciplina);
            }

            return disciplinasPendentes;
        }


    }
}