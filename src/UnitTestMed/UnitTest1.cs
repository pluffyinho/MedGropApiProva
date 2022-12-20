using AutoMapper;
using MedGrupo.API.Controllers;
using MedGrupo.Business.Interfaces;
using MedGrupo.Business.Models;
using MedGrupo.Business.ViewModel;
using NSubstitute;
using NuGet.ContentModel;
using FluentAssertions;
using AutoFixture;


namespace UnitTestMed
{
    public class ContactControllerTest
    {
        private readonly IMapper _mapper;
        private readonly IContactService _contactService;

        ContactController _sut;
        Fixture _fixture;
        public ContactControllerTest()
        {
            _fixture = new Fixture();
            _mapper = Substitute.For<IMapper>();
            _contactService = Substitute.For<IContactService>();
            _sut = new ContactController(_contactService, _mapper);
        }
        [Fact]
        public void GetAllContacts_ShouldReturnAllContact()
        {
            //Arrange
            IEnumerable<ContactViewModel> listcontactsvm2 = _fixture.Create<List<ContactViewModel>>();
            IEnumerable<ContactViewModel> listcontacts = new List<ContactViewModel>();
            _contactService.GetContatoAtivoList().Returns(listcontacts);
            _mapper.Map<IEnumerable<ContactViewModel>>(listcontacts).Returns(listcontactsvm2);
            //Act
            var result = _sut.GetAllContacts();
            //Assert
            Asset.Equals(result.IsCompleted, true);
            result.Result.Should().BeAssignableTo<IEnumerable<ContactViewModel>>();

        }
        [Fact]
        public void CreateContact_ShouldReturnCreateContact()
        {
            //Arrange
            var contactviewmodel = new ContactViewModel
            {
                Id = Guid.NewGuid(),
                Name = "Enzo",
                Birthdate = Convert.ToDateTime("1999-06-16"),
                Genre = Genre.Masculino,
                IsActive = true
            }; 
            _contactService.AddContact(contactviewmodel).Returns(contactviewmodel);
            
            //Act
            var result = _sut.CreateContact(contactviewmodel);
            //Asset
            Asset.Equals(result.IsCompleted, true);
            result.Result.Should().Be(contactviewmodel);
        }
    }
}