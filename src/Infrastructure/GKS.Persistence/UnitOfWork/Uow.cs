using GKS.Application.Abstrations.Repositories;
using GKS.Application.Abstrations.UnitOfWork;
using GKS.Domain.Entities.Common;
using GKS.Persistence.Contexts;
using GKS.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKS.Persistence.UnitOfWork
{
    public class Uow : IUow
    {
        private readonly GKSContext _context;

        public Uow(GKSContext context)
        {
            _context = context;
        }

        public ICommandRepository<T> GetCommandRepository<T>() where T : BaseEntity
        {
            return new CommandRepository<T>(_context);
        }

        public IQueryRepository<T> GetQueryRepository<T>() where T : BaseEntity
        {
            return new QueryRepository<T>(_context);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
