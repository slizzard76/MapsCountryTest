using DataAccess.Context.BaseEntity;
using MapsCountryTest.Api.BusinessLogic.MapCountryTest.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MapsCountryTest.Api.BusinessLogic.MapCountryTest.Repository.Implementation
{
    /// <summary>
    /// Базовый репозиторий для работы с сущностями в контексте базы данных.
    /// Предоставляет общие методы для CRUD операций.
    /// </summary>
    /// <typeparam name="TContext">Тип DbContext, используемый для доступа к базе данных.</typeparam>
    /// <typeparam name="TEntity">Тип сущности, которую репозиторий будет представлять.</typeparam>
    public abstract class BaseRepository<TContext, TEntity> : IBaseRepository<TEntity>
        where TEntity : BaseEntity
        where TContext : DbContext
    {
        // Контекст базы данных.
        private readonly TContext _dbContext;

        // DbSet для работы с сущностями данного типа.
        protected readonly DbSet<TEntity> _dbSet;

        /// <summary>
        /// Конструктор BaseRepository.
        /// </summary>
        /// <param name="dbContext">Экземпляр DbContext для работы с базой данных.</param>
        public BaseRepository(TContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        /// <summary>
        /// Асинхронно получает сущность по ее идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сущности.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Задача, возвращающая найденную сущность или null.</returns>
        public async Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        /// <summary>
        /// Асинхронно проверяет, существует ли сущность с заданным идентификатором.
        /// </summary>
        /// <param name="id">Идентификатор сущности.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Задача, возвращающая true, если сущность существует, иначе false.</returns>
        public async Task<bool> IsExistsAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbSet.AnyAsync(x => x.Id == id, cancellationToken);
        }

        /// <summary>
        /// Асинхронно сохраняет все изменения в базе данных.
        /// </summary>/// <param name="cancellationToken">Токен отмены операции.</param>
        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Синхронно сохраняет все изменения в базе данных.
        /// </summary>
        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Получает IQueryable всех сущностей с заданным фильтром.
        /// </summary>/// <param name="filter">Лямбда-выражение для фильтрации сущностей.</param>/// <returns>IQueryable отфильтрованных сущностей.</returns>
        public IQueryable<TEntity> GetAllFiltered(Expression<Func<TEntity, bool>> filter)
        {
            return _dbSet.Where(filter);
        }

        /// <summary>
        /// Получает IQueryable всех сущностей данного типа.
        /// </summary>/// <returns>IQueryable всех сущностей.</returns>
        public IQueryable<TEntity> GetAll()
        {
            return _dbSet;
        }
    }   
}
