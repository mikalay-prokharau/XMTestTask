using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using XmTestTask.Core.Entities;
using XmTestTask.Core.Interfaces;

namespace XmTestTask.Infrastructure.Data
{
    public class BTCRepository : RepositoryBase<BTCPrice>, IBTCRepository
    {
        public BTCRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<BTCPrice>> GetByDateRangeAsync(int startDate, int endDate, CancellationToken cancellationToken = default)
        {
            return await dbContext.BTCPrices.AsNoTracking().Where(c => c.Date >= startDate && c.Date <= endDate).ToListAsync(cancellationToken);
        }


        public async Task CreateOrUpdateAsync(BTCPrice entity, CancellationToken cancellationToken = default)
        {
            await dbContext.BTCPrices.Upsert(entity).RunAsync(cancellationToken);
        }
    }
}
