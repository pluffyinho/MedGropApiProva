using MedGrupo.Business.Interfaces;
using MedGrupo.Business.Models;
using MedGrupo.Business.ViewModel;


namespace MedGrupo.Business.Services
{
    public class ValidationService : IValidationService
    {
        protected static DateTime Now { get; set; } = DateTime.Now;



        public ErrorMessage ValidationAge(ContactViewModel contactViewModel)
        {
            var valid = new ErrorMessage() { Valid = true };
            if (contactViewModel.Birthdate > DateTime.Now)
            {
                valid = new ErrorMessage() { Valid = false, Error = "Data de nascimento maior que a atual" };
                return valid;
            }
            if (contactViewModel.Age < 18)
            {
                valid = new ErrorMessage() { Valid = false, Error = "Não é permitido cadastro de menores de idade." };
                return valid;
            }
             return valid;

        }
     
        public int CalculateAge(ContactViewModel contactViewModel)
        {
            var checkYear = contactViewModel.Birthdate;
            var days = DateTime.Now.Year - checkYear.Year;
            if (DateTime.Now.DayOfYear < checkYear.DayOfYear)
            {
                days = days - 1;
            }
            return days;
        }

        public IEnumerable<ContactViewModel> CalculateAgeList(IEnumerable<ContactViewModel> contactList)
        {
            var ageCalculateList = new List<ContactViewModel>();
            foreach (var contact in contactList)
            {
                contact.Age = GetAge(contact.Birthdate);
                ageCalculateList.Add(contact);
            }
            return ageCalculateList;
        }
        private static int GetAge(DateTime birthdate)
        {
            var days = Now - birthdate;
            return days.Days / Now.DayOfYear;
        }
    }
}
