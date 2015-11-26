using System.Collections.Generic;
using System.Web;
using Dominio.Aconselhador;

namespace Web.Models
{
    public class AconselhadorModel
    {
        private HttpContextBase _httpContext;
        private string _matricula;

        private List<Periodo> _restricoes;

        public AconselhadorModel(HttpContextBase httpContext, string matricula)
        {
            _httpContext = httpContext;
            _matricula = matricula;

            _restricoes = new List<Periodo>();
        }

        public Matricula GetMatricula()
        {
            var leitor = new LeitorCSV(GetFilePath());
            var creditosCursados = 0;
            var disciplinasPendentes = leitor.CarregaDisciplinasPendentes(out creditosCursados);
            var aconselhador = new Aconselhador(disciplinasPendentes, _restricoes, creditosCursados);

            return aconselhador.GetMatricula();
        }

        private string GetFilePath()
        {
            return _httpContext.Server.MapPath(string.Format("~/App_Data/{0}.csv", _matricula));
        }
    }
}