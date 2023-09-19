namespace _1p_atom_carmanager.backend.core.Entities;

/// <summary>
/// Тип автомобиля
/// </summary>
public class CarType
{
    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Заголловок типа
    /// </summary>
    public string Title { get; set; } = string.Empty;
}