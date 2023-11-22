using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface ISaleDetailRepositoryAsync : IGenericRepositoryAsync<SaleDetail>
    {
        Task<List<SaleDetail>> GetSalesByProductAsync(int productId);
    }
}
