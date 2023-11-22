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
    public class SaleMasterRepositoryAsync : GenericRepositoryAsync<SaleMaster>, ISaleMasterRepositoryAsync
    {
        private readonly DbSet<SaleMaster> _saleMaster;

        public SaleMasterRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _saleMaster = dbContext.Set<SaleMaster>();
        }
    }
}
