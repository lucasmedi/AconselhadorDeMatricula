using Dominio.Modelos;

namespace Web.Models
{
    public class AlunoViewModel
    {
        public Aluno _aluno;

        public AlunoViewModel(Aluno aluno)
        {
            _aluno = aluno;
        }

        public Aluno GetAluno()
        {
            return _aluno;
        }

        public GradeViewModel GetGrade()
        {
            return new GradeViewModel(_aluno.Grade);
        }

        public bool TemGrade
        {
            get { return _aluno.Grade != null; }
        }
    }
}