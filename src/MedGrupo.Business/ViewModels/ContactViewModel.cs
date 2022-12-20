using MedGrupo.Business.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedGrupo.Business.ViewModel
{
    public class ContactViewModel : Entity
    {
        [Display(Name = "Nome do Contato")]
        public string? Name { get; set; }
        [Display(Name = "Data de Nascimento")]
        public DateTime Birthdate { get; set; }
        [Display(Name = "Sexo")]
        public Genre Genre { get; set; }
        [Display(Name = "Ativo")]
        public bool IsActive { get; set; }
        [Display(Name = "Idade")]
        public int Age { get; set; }

        public string? MsgErro { get; set; }
        public bool Valid { get; set; } = true;
    }
}
