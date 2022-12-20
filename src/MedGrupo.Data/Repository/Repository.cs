using MedGrupo.Business.Interfaces;
using MedGrupo.Business.Models;
using MedGrupo.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MedGrupo.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly MeuDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(MeuDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> FindEntity(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdEntity(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<List<TEntity>> GetAllEntity()
        {
            return await DbSet.ToListAsync();
        }
        public virtual async Task<TEntity> Added(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentException("N�o pode adicionar entidade nula");

            var entityEntry = await DbSet.AddAsync(entity);
            Db.SaveChanges();

            return entityEntry.Entity;
        }

        public virtual async Task Updating(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }

        public virtual async Task Delete(Guid id)
        {
            DbSet.Remove(new TEntity { Id = id });
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }
        public IQueryable<TEntity> AsQueryable() => DbSet;
        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}
