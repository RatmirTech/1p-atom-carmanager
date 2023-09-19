using _1p_atom_carmanager.backend.core.Entities;
namespace _1p_atom_carmanager.backend.core.Requests
{
    /// <summary>
    /// Добавить новую машину
    /// Коммменты смотреть на <see cref="Car"/>
    /// </summary>
    public class RegNewCarRequest
    {
        public string LicensePlate { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public DateTime Year { get; set; }

        public string CarType { get; set; }

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
        public double DistanceTraveled { get; set; }
        public float Rpm { get; set; }
        public double HighResolutionVehicleDistance { get; set; }
        public double ServiceDistance { get; set; }
        public double EngineCoolantTemperature { get; set; }
    }
    /// <summary>
    /// Добавить инфу по тахографу
    /// <see cref="TachographInfo"/>
    /// </summary>
    public class RegCarTachographInfo
    {
        public double VehicleSpeed { get; set; }

        public double DistanceTraveled { get; set; }

        public float Rpm { get; set; }
    }
}
