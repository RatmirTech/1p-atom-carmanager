namespace _1p_atom_carmanager.backend.core.Entities;

/// <summary>
/// Информация с тахографа
/// </summary>
public class TachographInfo
{
    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Скорость
    /// </summary>
    public double VehicleSpeed { get; set; }

    /// <summary>
    /// Пройденное расстояние
    /// </summary>
    public double DistanceTraveled { get; set; }

    /// <summary>
    /// обороты в минуту
    /// </summary>
    public float Rpm { get; set; }
}
