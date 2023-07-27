namespace XmTestTask.Core.Interfaces
{
    /// <summary>
    /// <para>
    /// A <see cref="IRepositoryBase{T}" /> is used to get by id and save entities of the type <typeparamref name="T" />.
    /// </para>
    /// </summary>
    /// <typeparam name="T">The type of entity for repository.</typeparam>
    public interface IRepositoryBase<T> where T : class
    {
        /// <summary>
        /// Gets an entity with the primary key .
        /// </summary>
        /// <typeparam name="TId">The type of the primary key.</typeparam>
        /// <param name="id">The value of the primary key.</param>
        /// <returns>Task with the <typeparamref name="T"/> or <see langword="null"./></returns>
        Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull;
    }
}
