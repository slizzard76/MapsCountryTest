using System.ComponentModel.DataAnnotations;

/// <summary>
/// Представляет собой географические координаты, связанные с конкретным офисом.
/// </summary>
public class Coordinates
{
    /// <summary>
    /// Идентификатор офиса, к которому относится данная координата.
    /// </summary>
    [Key]
    public int OfficeId { get; set; }
        
    /// <summary>
    /// Широта (Latitude) географической точки.
    /// </summary>
    public double Latitude { get; set; }
        
    /// <summary>
    /// Долгота (Longitude) географической точки.
    /// </summary>
    public double Longitude { get; set; }
}

