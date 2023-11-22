using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class SaleDetailRepositoryAsync : GenericRepositoryAsync<SaleDetail>, ISaleDetailRepositoryAsync
    {
        private readonly DbSet<SaleDetail> _saleDetails;

        public SaleDetailRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _saleDetails = dbContext.Set<SaleDetail>();
        }

        public async Task<List<SaleDetail>> GetSalesByProductAsync(int productId)
        {
            return await _saleDetails
            .Where(sale => sale.ProductId == productId)
            .ToListAsync();
        }
    }
}
