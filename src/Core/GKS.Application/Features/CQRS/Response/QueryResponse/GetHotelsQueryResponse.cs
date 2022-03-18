using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKS.Application.Features.CQRS.Response.QueryResponse
{
    public class GetHotelsQueryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public string WebSite { get; set; }
    }
}
