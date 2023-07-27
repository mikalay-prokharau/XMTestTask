using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmTestTask.Core.Entities;

namespace XmTestTask.Core.Interfaces
{
    /// <summary>
    /// <para>
    /// A <see cref="IBTCRepository" /> is used to get by id and range and save entities of the type <see cref="BTCPrice" />.
    /// </para>
    /// </summary>
    public interface IBTCRepository : IRepositoryBase<BTCPrice>
    {
        /// <summary>
        /// Gets entities with the date range.
        /// </summary>
        /// <param name="startDate">The start date of the range.</param>
        /// <param name="endDate">The end date of the range.</param>
        /// <returns>Task with the List<typeparamref name="T"/> /></returns>
        Task<List<BTCPrice>> GetByDateRangeAsync(int startDate, int endDate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Create or update an entity in the storage.
        /// </summary>
        /// <param name="entity">The entity to create or update.</param>
        /// <returns>Task.</returns>
        Task CreateOrUpdateAsync(BTCPrice entity, CancellationToken cancellationToken = default);
    }
}
