using _1p_atom_carmanager.backend.core.Entities;
using _1p_atom_carmanager.backend.core.Requests;

namespace _1p_atom_carmanager.backend.core.Abstractions;

public interface ICarManagerService
{
    Task<string> RegNewCar(RegNewCarRequest car);
    Task<string> UpdateCarInfoByVin(UpdateCartInfoByVin info);
    Task<string> UpdateCarInfoByLicensePlate(UpdateCartInfoByLicensePlate info);
    Task<string> RemoveCarByVin(string vin);
    Task<List<Car>> GetAllCars();
    Task<Car> GetCarByVin(string vin);
    Task<Car> GetCarByLicensePlate(string licensePlate);
    Task<string> AddCarType(string type);
    Task<List<CarType>> GetCarTypes();
}