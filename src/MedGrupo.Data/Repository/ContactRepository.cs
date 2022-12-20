using MedGrupo.Business.Interfaces;
using MedGrupo.Business.Models;
using MedGrupo.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedGrupo.Data.Repository
{
    public class ContactRepository : Repository<Contact>, IContactRepository
    {
        public ContactRepository(MeuDbContext db) : base(db) { }
    }
}
