namespace _1p_atom_carmanager.backend.core.Requests
{
    public class UpdateCartInfoByLicensePlate
    {
        public string LicensePlate { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public DateTime Year { get; set; }

        public string SerialNumber { get; set; } = string.Empty;

        public string CarType { get; set; }

        public double VehicleImprovement { get; set; }
        public double VehicleSpeedWheelBased { get; set; }
        public double VehicleSpeedFromTachograph { get; set; }
        public bool ClutchSwitch { get; set; }
        public bool BrakeSwitch { get; set; }
        public bool CruiseControl { get; set; }
        public string PTOStatusMode { get; set; }
        public double AcceleratorPedalPosition { get; set; }
        public double TotalFuelUsed { get; set; }
        public float FuelLevel { get; set; }
        public double EngineSpeed { get; set; }
        public double GrossAxleWeightRating { get; set; }
        public double TotalEngineHours { get; set; }
        public string FMSStandardSoftwareVersion { get; set; }
        public string VehicleIdentificationNumber { get; set; }
        public double DistanceTraveled { get; set; }
        public float Rpm { get; set; }
        public double HighResolutionVehicleDistance { get; set; }
        public double ServiceDistance { get; set; }
        public double EngineCoolantTemperature { get; set; }
        public Guid TachographId { get; set; }
    }
}
