using AutoMapper;
using MedGrupo.Business.Models;
using MedGrupo.Business.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedGrupo.Business.ConfigAutoMapper
{
    public class ConfigAutoMapper : Profile
    {
        public ConfigAutoMapper()
        {
            CreateMap<Contact, ContactViewModel>().ReverseMap();
        }
    }
}
