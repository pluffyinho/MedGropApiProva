using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedGrupo.Business.Models
{
    public class Contact : Entity
    {
        public string? Name { get; set; }
        public bool IsActive { get; set; }
        public Genre Genre { get; set; }
        public DateTime Birthdate { get; set; }

        [NotMapped]
        public int Age { get; set; }

    }
}
