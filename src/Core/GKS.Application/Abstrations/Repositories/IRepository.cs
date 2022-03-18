using GKS.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKS.Application.Abstrations.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        DbSet<T> Table { get;}   
    }
}
