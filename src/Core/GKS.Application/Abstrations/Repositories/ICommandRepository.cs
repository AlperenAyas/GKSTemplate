using GKS.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKS.Application.Abstrations.Repositories
{
    public interface ICommandRepository<T> : IRepository<T> where T : BaseEntity
    {
        #region Syncronics
        T Add(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        #endregion
        #region Asyncronics
        Task<T> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        #endregion
    }
}
