using GKS.Application.Features.CQRS.Requests.QueryRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKS.Application.Abstrations.Services
{
    public interface IJwtService
    {
        string GenerateToken(LoginQueryRequest request);
    }
}
