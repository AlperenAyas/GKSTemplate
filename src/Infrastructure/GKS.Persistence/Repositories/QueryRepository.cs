using Dapper;
using GKS.Application.Abstrations.Repositories;
using GKS.Domain.Entities.Common;
using GKS.Domain.Queries.Markup;
using GKS.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GKS.Persistence.Repositories
{
    public class QueryRepository<T> : IQueryRepository<T> where T : BaseEntity
    {
        private readonly GKSContext _context;
        private readonly IConfiguration _configuration;

        public QueryRepository(GKSContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAllByFilter(Expression<Func<T, bool>> filter = null, Expression<Func<T, object>>[] includes = null, QueryTrackingBehavior isTracking = QueryTrackingBehavior.NoTracking)
        {
            var operationData = Table.AsTracking(isTracking);
            if (includes != null)
            {
                operationData = includes.Aggregate(operationData, (current, include) => current.Include(include));
            }
            if (filter != null)
            {
                operationData = operationData.Where(filter);
            }
            return operationData;
        }

        public async Task<IQueryable<T>> GetAllByFilterAsync(Expression<Func<T, bool>> filter = null, Expression<Func<T, object>>[] includes = null, QueryTrackingBehavior isTracking = QueryTrackingBehavior.NoTracking)
        {
            var operationData = Table.AsTracking(isTracking);
            if (includes != null)
            {
                operationData = includes.Aggregate(operationData, (current, include) => current.Include(include));
            }
            if (filter != null)
            {
                operationData = await Task.Run(() => operationData.Where(filter));
            }
            return operationData;
        }

        public T GetByFilter(Expression<Func<T, bool>> filter, Expression<Func<T, object>>[] includes = null, QueryTrackingBehavior isTracking = QueryTrackingBehavior.TrackAll)
        {
            var operationData = Table.AsTracking(isTracking);
            if (includes != null)
            {
                operationData = includes.Aggregate(operationData, (current, include) => current.Include(include));
            }

            return operationData.FirstOrDefault(filter);
        }

        public async Task<T> GetByFilterAsync(Expression<Func<T, bool>> filter, Expression<Func<T, object>>[] includes = null, QueryTrackingBehavior isTracking = QueryTrackingBehavior.TrackAll)
        {
            var operationData = Table.AsTracking(isTracking);
            if (includes != null)
            {
                operationData = includes.Aggregate(operationData, (current, include) => current.Include(include));
            }

            return await operationData.FirstOrDefaultAsync(filter);
        }

        public async Task<IEnumerable<TQuery>> RawQueryAsync<TQuery>(string query) where TQuery : IQuery
        {

            using(var connection = new NpgsqlConnection(_configuration.GetConnectionString("Gks")))
            {
                await connection.OpenAsync();

                IEnumerable<TQuery> data = await connection.QueryAsync<TQuery>(query);

                await connection.CloseAsync();

                return data;
            }
            
        }
        /// <summary>
        /// Sql Query Data Reader and Mapper
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static List<T> DataReaderMapToList<T>(IDataReader dr)
        {
            List<T> list = new List<T>();
            T obj = default(T);
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (!object.Equals(dr[prop.Name], DBNull.Value))
                    {
                        prop.SetValue(obj, dr[prop.Name], null);
                    }
                }
                list.Add(obj);
            }
            return list;
        }
    }
}
