namespace DataAccess.Context.BaseEntity
{
    /// <summary>
    /// Базовая сущность
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }
    }
    public abstract class BaseEntity<T>
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public virtual T Id { get; set; }
    }
}
