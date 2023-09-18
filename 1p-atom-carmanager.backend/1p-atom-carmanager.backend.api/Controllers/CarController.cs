using _1p_atom_carmanager.backend.core.Abstractions;
using _1p_atom_carmanager.backend.core.Entities;
using _1p_atom_carmanager.backend.core.Requests;
using Microsoft.AspNetCore.Mvc;

namespace _1p_atom_carmanager.backend.api.Controllers
{
    [ApiController]
    [Route("info")]
    public class CarController : ControllerBase
    {
        private readonly ICarManagerService _carManagerService;

        public CarController(ICarManagerService carManagerService)
        {
            _carManagerService = carManagerService;
        }

        [HttpGet(nameof(GetAllCars))]
        public Task<List<Car>> GetAllCars()
        {
            return _carManagerService.GetAllCars();
        }

        [HttpGet(nameof(GetCarByLicensePlate))]
        public Task<Car> GetCarByLicensePlate(string licensePlate)
        {
            return _carManagerService.GetCarByLicensePlate(licensePlate);
        }

        [HttpGet(nameof(GetCarByVin))]
        public Task<Car> GetCarByVin(string vin)
        {
            return _carManagerService.GetCarByVin(vin);
        }

        [HttpPost(nameof(GetCarByVin))]
        public Task<string> RegNewCar(RegNewCarRequest car)
        {
            return _carManagerService.RegNewCar(car);
        }

        [HttpDelete(nameof(RemoveCarByVin))]
        public Task<string> RemoveCarByVin(string vin)
        {
            return _carManagerService.RemoveCarByVin(vin);
        }

        [HttpPut(nameof(UpdateCarInfoByLicensePlate))]
        public Task<string> UpdateCarInfoByLicensePlate(UpdateCartInfoByLicensePlate info)
        {
            return _carManagerService.UpdateCarInfoByLicensePlate(info);
        }

        [HttpPut(nameof(UpdateCarInfoByVin))]
        public Task<string> UpdateCarInfoByVin(UpdateCartInfoByVin info)
        {
            return _carManagerService.UpdateCarInfoByVin(info);
        }

        [HttpPost(nameof(AddCarType))]
        public Task<string> AddCarType(string type)
        {
            return _carManagerService.AddCarType(type);
        }

        [HttpGet(nameof(GetCarTypes))]
        public Task<List<CarType>> GetCarTypes()
        {
            return _carManagerService.GetCarTypes();
        }
    }
}
