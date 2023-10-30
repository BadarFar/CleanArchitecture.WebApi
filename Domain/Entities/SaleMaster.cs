using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class SaleMaster : AuditableBaseEntity
    {
        public string CustomerName { get; set; }
        public string OrderStatus { get; set; }
        public decimal GST { get; set; }

        public List<SaleDetail> SaleDetails { get; set; }
    }
}
