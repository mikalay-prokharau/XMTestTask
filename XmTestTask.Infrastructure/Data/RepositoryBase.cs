﻿using XmTestTask.Core.Interfaces;

namespace XmTestTask.Infrastructure.Data
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly AppDbContext dbContext;

        public RepositoryBase(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull
        {
            return await dbContext.Set<T>().FindAsync(new object[] { id }, cancellationToken: cancellationToken);
        }
    }
}
