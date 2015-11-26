using System.ComponentModel.DataAnnotations;
using Dominio.Aconselhador;

namespace Web.Models
{
    public class SugerirViewModel
    {
        [Display(Name = "Restrições")]
        public string Restricoes { get; set; }

        public Matricula Matricula { get; internal set; }
    }
}