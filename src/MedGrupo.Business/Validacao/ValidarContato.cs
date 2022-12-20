using MedGrupo.Business.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedGrupo.Business.Validacao
{
    public class ValidarContato
    {

        public static ErrorMessage ValidarDados(ContactViewModel obj)
        {
            var validation = new ErrorMessage() { Valid = true };

            if (obj.Birthdate > DateTime.Now)
                validation = new ErrorMessage() { Valid = false, Error = "Data de nascimento é maior que a data atual" };

            if (obj.Age < 18)
                validation = new ErrorMessage() { Valid = false, Error = "O contato tem que ser maior de idade" };

            return validation;
        }

        public static int CalcularIdade(ContactViewModel obj)
        {
            var dataNascimento = obj.Birthdate;
            int idade = DateTime.Now.Year - dataNascimento.Year;
            if (DateTime.Now.DayOfYear < dataNascimento.DayOfYear)
            {
                idade = idade - 1;
            }
            return idade;
        }
    }
}
