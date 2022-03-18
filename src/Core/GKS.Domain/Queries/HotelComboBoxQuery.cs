using GKS.Domain.Queries.Markup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKS.Domain.Queries
{
    public class HotelComboBoxQuery : IQuery
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
