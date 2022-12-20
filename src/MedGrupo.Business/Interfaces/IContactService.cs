using MedGrupo.Business.Models;
using MedGrupo.Business.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MedGrupo.Business.Interfaces
{
    public interface IContactService 
    {
        Task<ContactViewModel> GetContactById(Guid id);
        Task<IEnumerable<ContactViewModel>> GetAllContact();
        Task <ContactViewModel> AddContact(ContactViewModel contact);
        Task <ContactViewModel> DeleteContact(Guid id);
        Task <ContactViewModel> UpdateContact(ContactViewModel contactViewModel);
        Task<IEnumerable<ContactViewModel>> GetContatoAtivoList();
        Task<Contact> DisableActive(Guid id);
    }
}
