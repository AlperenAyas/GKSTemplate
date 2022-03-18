using GKS.Application.Abstrations.Repositories;
using GKS.Domain.Entities.Common;
using GKS.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKS.Persistence.Repositories
{
    public class CommandRepository<T> : ICommandRepository<T> where T : BaseEntity
    {
        private readonly GKSContext _context;

        public CommandRepository(GKSContext context)
        {
            _context = context;
        }

        public DbSet<T> Table  => _context.Set<T>();

        public T Add(T entity)
        {
            Table.Add(entity);
            return entity;
        }

        public async Task<T> AddAsync(T entity)
        {
            await Table.AddAsync(entity);
            return entity;
        }

        public bool Delete(T entity)
        {
            bool value = false;
            try
            {
                Table.Remove(entity);
                value = true;
            }
            catch (Exception)
            {

            }
            return value;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            bool value = false;
            try
            {
                await Task.Run(() => Table.Remove(entity));
                value = true;
            }
            catch (Exception)
            {

            }
            return value;
        }

        public bool Update(T entity)
        {
            bool value = false;
            try
            {
                Table.Update(entity);
                value = true;
            }
            catch (Exception)
            {

            }
            return value;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            bool value = false;
            try
            {
                await Task.Run(() => Table.Update(entity));
                value = true;
            }
            catch (Exception)
            {

            }
            return value;
        }

    }
}
