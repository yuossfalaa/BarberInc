using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Services.DomainServices.Common
{
    public class NonQueryDataService<T> where T : DomainObject
    {
        private readonly DBContextFactory _contextFactory;

        public NonQueryDataService(DBContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public async Task<T> Create(T entity)
        {
            using (DBContext context = _contextFactory.CreateDbContext())
            {
                EntityEntry<T> createdResult = await context.Set<T>().AddAsync(entity);
                try
                {
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                }
                return createdResult.Entity;

            }
        }

        public async Task<T> Update(int id, T entity)
        {
            using (DBContext context = _contextFactory.CreateDbContext())
            {

                entity.Id = id;

                context.Set<T>().Update(entity);
                try
                {
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    string message = ex.ToString();
                }


                return entity;
            }
        }

        public async Task<bool> Delete(T Entity)
        {
            using (DBContext context = _contextFactory.CreateDbContext())
            {
                T entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == Entity.Id);
                context.Set<T>().Remove(entity);
                try
                {
                    await context.SaveChangesAsync();
                }
                catch
                {

                }

                return true;
            }
        }

    }
}
