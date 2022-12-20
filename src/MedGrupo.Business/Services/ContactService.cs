using AutoMapper;
using MedGrupo.Business.Interfaces;
using MedGrupo.Business.Models;
using MedGrupo.Business.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MedGrupo.Business.Services
{
    public class ContactService : IContactService
    {

        private readonly IContactRepository _contactRepository;

        private readonly IValidationService _validationService;
        private readonly IMapper _mapper;

        public ContactService(IContactRepository contactRepository, IValidationService validationService, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _validationService = validationService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ContactViewModel>> GetAllContact()
        {
            return _mapper.Map<IEnumerable<ContactViewModel>>(_contactRepository.GetAllEntity());
        }
        public async Task<ContactViewModel> GetContactById(Guid id)
        {
            var returnId = _contactRepository.GetByIdEntity(id).Result;
            return _mapper.Map<ContactViewModel>(returnId);
        }

        public async Task<ContactViewModel> AddContact(ContactViewModel contactViewModel)
        {
            var contactreturn = new ContactViewModel();

            contactViewModel.Age = _validationService.CalculateAge(contactViewModel);
            var valid = _validationService.ValidationAge(contactViewModel);

            if (!valid.Valid)
                contactreturn.MsgErro = "Erro na inclusão de dados! " + valid.Error;
            else
            {
                try
                {
                    var init = _mapper.Map<Contact>(contactViewModel);
                    var add = await _contactRepository.Added(init);
                    contactreturn = _mapper.Map<ContactViewModel>(init);
                }
                catch (Exception ex)
                {
                    contactreturn.MsgErro = "Erro na inclusão de dados! " + ex.Message;
                }
            }

            return contactreturn;

        }
        public async Task<ContactViewModel> DeleteContact(Guid id)
        {

            var returnDelete = new ContactViewModel();
            try
            {

                returnDelete.Valid = true;
                await _contactRepository.Delete(id);
            }
            catch (Exception ex)
            {
                returnDelete.Valid = false;
                returnDelete.MsgErro = "Erro na exclusão do contato! " + ex.Message;

            }

            return returnDelete;
        }

        public async Task<ContactViewModel> UpdateContact(ContactViewModel contactViewModel)
        {
            var contactreturn = new ContactViewModel();

            contactViewModel.Age = _validationService.CalculateAge(contactViewModel);
            var valid = _validationService.ValidationAge(contactViewModel);

            if (!valid.Valid)
            { contactViewModel.MsgErro = "Erro na inclusão de dados! " + valid.Error; contactViewModel.Valid = false; }
            else
            {
                try
                {
                    var alternative = _mapper.Map<Contact>(contactViewModel);
                    await _contactRepository.Updating(alternative);
                    contactViewModel.Valid = true;
                }
                catch (Exception ex)
                {
                    contactViewModel.Valid = false;
                    contactViewModel.MsgErro = "Erro na inclusão de dados" + ex.Message;
                }
            }

            return contactViewModel;

        }

        public async Task<IEnumerable<ContactViewModel>> GetContatoAtivoList()
        {
            var listcontact = await _contactRepository.GetAllEntity();
            var listcontactAtive = listcontact.Where(c => c.IsActive == true).ToList();
            var listMapHelper = _mapper.Map<IEnumerable<ContactViewModel>>(listcontactAtive);

            var result = _validationService.CalculateAgeList(listMapHelper);

            return result;
        }

        public async Task<Contact> DisableActive(Guid id)
        {
            var listcontato = await _contactRepository.GetAllEntity();
            var contact = listcontato.Where(c => c.Id == id).FirstOrDefault();
            if (contact.IsActive)
                contact.IsActive = false;
            else
                contact.IsActive = true;
            await _contactRepository.Updating(contact);
            return contact;
        }
    }
}
