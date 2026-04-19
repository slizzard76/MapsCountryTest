using DataAccess.Context.BaseEntity;

/// <summary>
///Телефон
///</summary>
public class Phone : BaseEntity
{
    /// <summary>
    /// Идентификатор офиса, к которому привязан телефон.
    ///</summary>
    public int OfficeId { get; set; }
    /// <summary>
    /// Основной номер телефона.
    ///</summary>
    public string PhoneNumber { get; set; }
    /// <summary>
    /// Дополнительная информация о телефоне (например, расширение).
    ///</summary>
    public string? Additional { get; set; }
}
