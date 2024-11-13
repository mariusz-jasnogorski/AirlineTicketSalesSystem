using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineTicketSalesSystem.Domain.Entities
{
    public class TenantGroupA : Tenant
    {
        public TenantGroupA(string name) : base(name) { }

        public override bool ShouldRecordDiscounts => true;
    }
}
