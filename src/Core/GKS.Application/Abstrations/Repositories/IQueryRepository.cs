using GKS.Domain.Entities.Common;
using GKS.Domain.Queries.Markup;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GKS.Application.Abstrations.Repositories
{
    public interface IQueryRepository<T> : IRepository<T> where T : BaseEntity
    {
        #region Syncronics
        IQueryable<T> GetAllByFilter(Expression<Func<T, bool>> filter = null, Expression<Func<T, object>>[] includes = null, QueryTrackingBehavior isTracking = QueryTrackingBehavior.NoTracking);
        T GetByFilter(Expression<Func<T, bool>> filter, Expression<Func<T, object>>[] includes = null, QueryTrackingBehavior isTracking = QueryTrackingBehavior.NoTracking);
        #endregion

        #region Asyncronics
        Task<IQueryable<T>> GetAllByFilterAsync(Expression<Func<T, bool>> filter = null, Expression<Func<T, object>>[] includes = null, QueryTrackingBehavior isTracking = QueryTrackingBehavior.NoTracking);
        Task<T> GetByFilterAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>>[] includes = null, QueryTrackingBehavior isTracking = QueryTrackingBehavior.NoTracking);
        Task<IEnumerable<TQuery>> RawQueryAsync<TQuery>(string query) where TQuery : IQuery;
        #endregion

        
    }
}
