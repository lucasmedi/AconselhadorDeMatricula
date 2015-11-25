using System;
using System.Collections.Generic;
using System.Linq;

namespace Dominio.Aconselhador
{
    public class Aconselhador
    {
        private IList<Disciplina> _pendentes;
        private IList<Periodo> _restricoes;

        private Dictionary<Disciplina, int> ranking;

        public Aconselhador(IList<Disciplina> pendentes, IList<Periodo> restricoes)
        {
            _pendentes = pendentes;
            _restricoes = restricoes;

            ranking = new Dictionary<Disciplina, int>();
        }

        public Matricula GetMatricula()
        {
            GeraRanking();

            if (ranking.Count == 0)
                throw new Exception("Não há disciplinas disponíveis para montar a grade de matrícula!");

            return GeraGrade();
        }

        private void GeraRanking()
        {
            foreach (var disciplina in _pendentes)
            {
                ranking.Add(disciplina, 100);
            }

            // Por nível
            var nivel = _pendentes.Min(o => o.Nivel);
            foreach (var disciplina in _pendentes)
            {
                if (disciplina.Nivel != nivel)
                {
                    ranking[disciplina] -= disciplina.Nivel - nivel;
                }
            }

            // Por número de pré-requisitos à frente
            var rankingVinculo = new Dictionary<Disciplina, int>();
            foreach (var disciplina in _pendentes)
            {
                var vinculadas = new List<Disciplina>();
                GetVinculados(vinculadas, disciplina.CodCred);
                rankingVinculo.Add(disciplina, vinculadas.Count);
            }

            var maxVinc = rankingVinculo.Max(o => o.Value);
            foreach (var rkVinc in rankingVinculo.Keys)
            {
                ranking[rkVinc] -= maxVinc - rankingVinculo[rkVinc];
            }

            // Remove bloqueadas
            foreach (var pend in _pendentes)
            {
                var depende = _pendentes.Where(o => o.Prerequisitos.Any(p => p.CodCred == pend.CodCred));
                if (!depende.Any())
                    continue;

                foreach (var item in depende)
                {
                    ranking.Remove(item);
                }
            }

            ranking = ranking.OrderByDescending(o => o.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        private Matricula GeraGrade()
        {
            var grade = new Matricula();

            var fila = new Queue<Disciplina>();
            var continua = true;

            foreach (var disciplina in ranking.Keys.ToList())
            {
                fila.Enqueue(disciplina);
            }

            do
            {
                if (fila.Any())
                {
                    grade.AdicionarDisciplina(fila.Dequeue());
                }
                else
                {
                    continua = false;
                }
            } while (continua);

            return grade;
        }

        private void GetVinculados(List<Disciplina> vinculadas, string codCred)
        {
            var vinc = _pendentes.Where(o => o.Prerequisitos.Any(p => p.CodCred == codCred));
            if (vinc.Any())
            {
                foreach (var v in vinc)
                {
                    if (!vinculadas.Any(o => o.CodCred == v.CodCred))
                    {
                        vinculadas.Add(v);
                    }
                }

                foreach (var item in vinc)
                {
                    GetVinculados(vinculadas, item.CodCred);
                }
            }
        }
    }
}