namespace _1p_atom_carmanager.backend.core.Entities;

/// <summary>
/// Машина
/// </summary>
public class Car
{
    /// <summary>
    /// ID
    /// </summary>
    public Guid Id { get; set; } = Guid.Empty;

    /// <summary>
    /// Номерной знак
    /// </summary>
    public string LicensePlate { get; set; } = string.Empty;

    /// <summary>
    /// Модель
    /// </summary>
    public string Model { get; set; } = string.Empty;

    /// <summary>
    /// Год выпуска
    /// </summary>
    public DateTime Year { get; set; }

    /// <summary>
    /// Тип машины
    /// </summary>
    public virtual CarType CarType { get; set; }

    /// <summary>
    /// Все нижеперечисленные переменные соотвествуют fmsi
    /// </summary>
    public double VehicleImprovement { get; set; }
    public double VehicleSpeedWheelBased { get; set; }
    public double VehicleSpeedFromTachograph { get; set; }
    public bool ClutchSwitch { get; set; }
    public bool BrakeSwitch { get; set; }
    public bool CruiseControl { get; set; }
    public string PTOStatusMode { get; set; } = string.Empty;
    public double AcceleratorPedalPosition { get; set; }
    public double TotalFuelUsed { get; set; }
    public float FuelLevel { get; set; }
    public double EngineSpeed { get; set; }
    public double GrossAxleWeightRating { get; set; }
    public double TotalEngineHours { get; set; }
    public string FMSStandardSoftwareVersion { get; set; } = string.Empty;
    public string VehicleIdentificationNumber { get; set; } = string.Empty;
    public virtual TachographInfo TachographInformation { get; set; }
    public double HighResolutionVehicleDistance { get; set; }
    public double ServiceDistance { get; set; }
    public double EngineCoolantTemperature { get; set; }
}
