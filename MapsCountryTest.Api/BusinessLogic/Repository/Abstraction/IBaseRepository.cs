using DataAccess.Context.BaseEntity;
using System.Linq.Expressions;

namespace MapsCountryTest.Api.BusinessLogic.MapCountryTest.Repository.Abstraction
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Получить сущность по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Сущность</returns>
        Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken);

       

        /// <summary>
        /// Проверить наличие сущности
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Признак наличие сущности</returns>
        Task<bool> IsExistsAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Сохранить изменения
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        Task SaveChangesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Сохранить изменения
        /// </summary>
        /// <returns></returns>
        void SaveChanges();

        /// <summary>
        /// Получить отфильтрованные данные
        /// </summary>
        /// <param name="filter">Фильтр</param>
        /// <returns></returns>
        IQueryable<TEntity> GetAllFiltered(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Получить все данные
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetAll();
    }
}
