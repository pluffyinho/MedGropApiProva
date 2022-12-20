using AutoMapper;
using MedGrupo.Business.Interfaces;
using MedGrupo.Business.Models;
using MedGrupo.Business.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedGrupo.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IContactService _contactService;
        public ContactController(IContactService contactService, IMapper mapper)
        {
            _contactService = contactService;
            _mapper = mapper;
        }
        [HttpGet("GetAll")]
        public async Task<IEnumerable<ContactViewModel>> GetAllContacts()
        {
                var contatos = await _contactService.GetContatoAtivoList();
                return contatos;
        }

        [HttpGet("Details/{id}")]
        public async Task<ActionResult<ContactViewModel>> GetContact(Guid id)
        {
            var result = await _contactService.GetContactById(id);
            if (!result.IsActive) 
            { 
                return BadRequest("Não é possivel acessar dados de contatos inativos!"); 
            }
            return Ok(result); 
        }
        [HttpPatch("DeactivateOrActive/{id}")]
        public async Task<ActionResult<ContactViewModel>> Deactivate(Guid id)
        {
            if (!ContactExist(id))
            {
                return NotFound("Contato inexistente!");

            }
            return _mapper.Map<ContactViewModel>(await _contactService.DisableActive(id));
        }
        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> EditContact(Guid id, ContactViewModel contactViewModel)
        {
            var model = _contactService.UpdateContact(contactViewModel).Result;

            if (model != null && model.Valid)
                return Ok(model);
            else
                return BadRequest(model.MsgErro);
        }
        [HttpPost("Create")]
        public async Task<ActionResult<ContactViewModel>> CreateContact(ContactViewModel contactViewModel)
        {
            var result = await _contactService.AddContact(contactViewModel);
            if (result != null && string.IsNullOrEmpty(result.MsgErro))
                return CreatedAtAction("GetContact", new { id = result.Id }, result);
            else
                return BadRequest(result.MsgErro);
            
        }
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteContact(Guid id)
        {
            var result = await _contactService.DeleteContact(id);

            if(result != null && result.Valid)
            {
                return Ok("Contato excluido!");
            }
            else return BadRequest(result.MsgErro);
        }
        private bool ContactExist(Guid id)
        {
            return (_contactService.GetContactById(id).Result != null) ? true : false;
        }
    }
}

