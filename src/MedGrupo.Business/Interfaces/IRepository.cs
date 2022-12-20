using MedGrupo.Business.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MedGrupo.Business.Interfaces
{
    public interface IRepository<TEntity>  where TEntity : Entity
    {
        Task <TEntity> Added(TEntity entity);
        Task<TEntity> GetByIdEntity(Guid id);
        Task<List<TEntity>> GetAllEntity();
        Task Updating(TEntity entity);
        Task Delete(Guid id);
        IQueryable<TEntity> AsQueryable();
        Task<IEnumerable<TEntity>> FindEntity(Expression<Func<TEntity, bool>> predicate);
        Task<int> SaveChanges();
    

    }
}
