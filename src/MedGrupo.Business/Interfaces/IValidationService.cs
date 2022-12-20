using MedGrupo.Business.Models;
using MedGrupo.Business.ViewModel;

namespace MedGrupo.Business.Interfaces
{
    public interface IValidationService
    {
        int CalculateAge(ContactViewModel contactViewModel);
        ErrorMessage ValidationAge(ContactViewModel contactViewModel);
        
        IEnumerable<ContactViewModel> CalculateAgeList(IEnumerable<ContactViewModel> contactList);
    }
}
