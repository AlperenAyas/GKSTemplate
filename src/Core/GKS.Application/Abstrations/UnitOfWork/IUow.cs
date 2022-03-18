using GKS.Application.Abstrations.Repositories;
using GKS.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKS.Application.Abstrations.UnitOfWork
{
    public interface IUow
    {
        ICommandRepository<T> GetCommandRepository<T>() where T : BaseEntity;
        IQueryRepository<T> GetQueryRepository<T>() where T : BaseEntity;
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
