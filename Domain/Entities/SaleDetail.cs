using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class SaleDetail : AuditableBaseEntity
    {
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [ForeignKey("SaleMaster")]
        public int SaleMasterId { get; set; }
        public string ProductName { get; set; }
        public decimal Qty { get; set; }
        public decimal Rate { get; set; }
        public decimal? Total { get; set; }
    }
}
