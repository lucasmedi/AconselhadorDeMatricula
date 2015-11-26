using System.ComponentModel.DataAnnotations;
using Dominio.Aconselhador;

namespace Web.Models
{
    public class SugerirViewModel
    {
        [Display(Name = "Restrições Ex.: 2LM,3NP,6LMNP")]
        public string Restricoes { get; set; }

        public Matricula Matricula { get; internal set; }
    }
}