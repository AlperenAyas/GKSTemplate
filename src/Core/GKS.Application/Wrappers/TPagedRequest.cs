using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKS.Application.Wrappers
{
    public class TPagedRequest<TRequest> where TRequest : class
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public TRequest Request { get; set; }
    }
}
