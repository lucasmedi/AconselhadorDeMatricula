﻿using System.Collections.Generic;
using System.Linq;

namespace Dominio.Aconselhador
{
    public class Matricula
    {
        private Celula[,] grade;
        private List<Disciplina> normais;
        private List<Disciplina> especiais;

        private int qtdCreditos;

        public Matricula()
        {
            Inicializa();
        }

        public Matricula(IList<Periodo> bloqueados)
        {
            Inicializa();

            PreencherRestricoes(bloqueados);
        }

        private void Inicializa()
        {
            qtdCreditos = 0;
            grade = new Celula[6, 7];
            normais = new List<Disciplina>();
            especiais = new List<Disciplina>();
        }

        public void PreencherRestricoes(IList<Periodo> bloqueados)
        {
            foreach (var periodo in bloqueados)
            {
                grade[(int)periodo.DiaSemana - 2, (int)periodo.Horario - 1] = new Celula()
                {
                    Bloqueado = true,
                };
            }
        }

        public bool AdicionarDisciplina(Disciplina disciplina)
        {
            if (disciplina == null)
                return false;

            if (!disciplina.Turmas.Any())
            {
                qtdCreditos += disciplina.Creditos;
                especiais.Add(disciplina);
                return false;
            }

            var periodosDisponiveis = true;
            var adicionouPeriodo = false;
            foreach (var turma in disciplina.Turmas)
            {
                foreach (var periodo in turma.Periodos)
                {
                    if (grade[(int)periodo.DiaSemana - 2, (int)periodo.Horario - 1] != null)
                    {
                        periodosDisponiveis = false;
                    }
                }

                if (!periodosDisponiveis)
                {
                    periodosDisponiveis = true;
                    continue;
                }

                foreach (var periodo in turma.Periodos)
                {
                    adicionouPeriodo = true;
                    grade[(int)periodo.DiaSemana - 2, (int)periodo.Horario - 1] = new Celula()
                    {
                        Bloqueado = false,
                        CodCred = disciplina.CodCred,
                        Turma = turma.Numero
                    };

                    disciplina.TurmaSelecionada = turma;
                }

                break;
            }

            if (!adicionouPeriodo)
            {
                return false;
            }

            qtdCreditos += disciplina.Creditos;
            normais.Add(disciplina);

            return true;
        }

        public Celula[,] GetGrade()
        {
            var dia = new Dictionary<int, bool>();
            var horario = new Dictionary<int, bool>();

            for (int i = 0; i < grade.GetLength(0); i++)
            {
                var temNoDia = false;
                for (int j = 0; j < grade.GetLength(1); j++)
                {
                    if (grade.GetValue(i, j) != null)
                    {
                        temNoDia = true;
                    }
                }

                if (temNoDia)
                    dia.Add(i, true);
            }

            for (int i = 0; i < grade.GetLength(1); i++)
            {
                var temNoHorario = false;
                for (int j = 0; j < grade.GetLength(0); j++)
                {
                    if (grade.GetValue(j, i) != null)
                    {
                        temNoHorario = true;
                    }
                }

                if (temNoHorario)
                    horario.Add(i, true);
            }

            foreach (var diaKey in dia.Keys)
            {
                foreach (var horarioKey in horario.Keys)
                {
                    if (grade.GetValue(diaKey, horarioKey) == null)
                    {
                        grade[diaKey, horarioKey] = new Celula() { Bloqueado = true };
                    }
                }
            }

            return grade;
        }

        public IList<Disciplina> GetDisciplinas()
        {
            var todas = new List<Disciplina>();
            todas.AddRange(normais);
            todas.AddRange(especiais);
            return todas;
        }

        private Disciplina BuscaDisciplina(string codcred)
        {
            var d = normais.Where(o => o.CodCred == codcred).FirstOrDefault();

            if (d == null)
                d = especiais.Where(o => o.CodCred == codcred).FirstOrDefault();

            return d;
        }

        
    }
}