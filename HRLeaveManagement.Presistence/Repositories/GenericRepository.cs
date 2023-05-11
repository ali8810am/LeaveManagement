using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveManagement.Application.Contracts.Presistence;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Presistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<T> Add(T entity)
        {
            await _context.AddAsync(entity);
           
            return entity;
        }

        public async Task Delete(T entity)
        {
            try
            {
                _context.Set<T>().Remove(entity);
            
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<bool> Exists(int id)
        {
            var entity =await Get(id);
            bool exist = !(entity == null);
            return exist;
        }


        public async Task<T> Get(int id)
        {
               var entity= await _context.Set<T>().FindAsync(id);
               return entity;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task Update(T entity)
        {
        _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
