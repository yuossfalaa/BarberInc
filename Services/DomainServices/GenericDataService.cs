using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using EntityFramework;
using Microsoft.EntityFrameworkCore;
using Services.DomainServices.Common;

namespace Services.DomainServices
{
    public class GenericDataService<T> : IDataService<T> where T : DomainObject
    {
        private readonly DBContextFactory _contextFactory;
        private readonly NonQueryDataService<T> _nonQueryDataService;

        public GenericDataService(DBContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _nonQueryDataService = new NonQueryDataService<T>(contextFactory);
        }

        public async Task<T> Create(T entity)
        {
            return await _nonQueryDataService.Create(entity);
        }

        public async Task<bool> Delete(T entity)
        {
            return await _nonQueryDataService.Delete(entity);
        }
        public async Task<T> Update(int id, T entity)
        {
            return await _nonQueryDataService.Update(id, entity);
        }

        public async Task<T> Get(int id)
        {
            using (DBContext context = _contextFactory.CreateDbContext())
            {
                try
                {
                    T entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
                    return entity;


                }
                catch
                {

                }
                return Activator.CreateInstance<T>();
            }
        }

        public async Task<List<T>> GetAll()
        {
            using (DBContext context = _contextFactory.CreateDbContext())
            {
                try
                {
                    List<T> entities = await context.Set<T>().ToListAsync();
                    return entities;

                }
                catch
                {

                }
                return Activator.CreateInstance<List<T>>();
            }
        }
    }
}
