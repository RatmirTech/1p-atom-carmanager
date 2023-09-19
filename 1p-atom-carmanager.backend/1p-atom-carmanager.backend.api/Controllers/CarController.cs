using _1p_atom_carmanager.backend.core.Abstractions;
using _1p_atom_carmanager.backend.core.Entities;
using _1p_atom_carmanager.backend.core.Requests;
using Microsoft.AspNetCore.Mvc;

namespace _1p_atom_carmanager.backend.api.Controllers
{
    /// <summary>
    /// Контроллер для запросов
    /// </summary>
    [ApiController]
    [Route("car")]
    public class CarController : ControllerBase
    {
        /// <summary>
        /// Сервис для управления автопарком
        /// </summary>
        private readonly ICarManagerService _carManagerService;

        /// <summary>
        /// Коструктор
        /// </summary>
        /// <param name="carManagerService">Сервис для управления автопарком</param>
        public CarController(ICarManagerService carManagerService)
        {
            _carManagerService = carManagerService;
        }

        /// <summary>
        /// Получить все машины
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetAllCars))]
        public Task<List<Car>> GetAllCars()
        {
            return _carManagerService.GetAllCars();
        }

        /// <summary>
        /// Получить машину по номеру
        /// </summary>
        /// <param name="licensePlate">номер</param>
        /// <returns>объект машины</returns>
        [HttpGet(nameof(GetCarByLicensePlate))]
        public Task<Car> GetCarByLicensePlate(string licensePlate)
        {
            return _carManagerService.GetCarByLicensePlate(licensePlate);
        }

        /// <summary>
        /// Получить машину по VIN
        /// </summary>
        /// <param name="vin">vin</param>
        /// <returns>объект машины</returns>
        [HttpGet(nameof(GetCarByVin))]
        public Task<Car> GetCarByVin(string vin)
        {
            return _carManagerService.GetCarByVin(vin);
        }

        /// <summary>
        /// Зарегистрировать новую машину
        /// </summary>
        /// <param name="carRequest">запрос</param>
        /// <returns>результат</returns>
        /// <exception cref="Exception"></exception>
        [HttpPost(nameof(RegNewCar))]
        public Task<string> RegNewCar([FromForm] RegNewCarRequest carRequest)
        {
            return _carManagerService.RegNewCar(carRequest);
        }

        /// <summary>
        /// Удалить машину по vin
        /// </summary>
        /// <param name="vin">vin</param>
        /// <returns>результат</returns>
        /// <exception cref="Exception"></exception>
        [HttpDelete(nameof(RemoveCarByVin))]
        public Task<string> RemoveCarByVin(string vin)
        {
            return _carManagerService.RemoveCarByVin(vin);
        }

        /// <summary>
        /// Обновить информацию о машине по номеру
        /// </summary>
        /// <param name="info">запрос</param>
        /// <returns>результат</returns>
        /// <exception cref="Exception"></exception>
        [HttpPut(nameof(UpdateCarInfoByLicensePlate))]
        public Task<string> UpdateCarInfoByLicensePlate([FromForm] UpdateCartInfoByLicensePlate info)
        {
            return _carManagerService.UpdateCarInfoByLicensePlate(info);
        }

        /// <summary>
        /// Обновить информацию о машине по vin
        /// </summary>
        /// <param name="info">запрос</param>
        /// <returns>результат</returns>
        /// <exception cref="Exception"></exception>
        [HttpPut(nameof(UpdateCarInfoByVin))]
        public Task<string> UpdateCarInfoByVin([FromForm] UpdateCartInfoByVin info)
        {
            return _carManagerService.UpdateCarInfoByVin(info);
        }

        /// <summary>
        /// Добавить тип машины
        /// </summary>
        /// <param name="type">Тип</param>
        /// <returns>результат</returns>
        /// <exception cref="Exception"></exception>
        [HttpPost(nameof(AddCarType))]
        public Task<string> AddCarType(string type)
        {
            return _carManagerService.AddCarType(type);
        }

        /// <summary>
        /// Получить все типы машин
        /// </summary>
        /// <returns>список типов машин</returns>
        [HttpGet(nameof(GetCarTypes))]
        public Task<List<CarType>> GetCarTypes()
        {
            return _carManagerService.GetCarTypes();
        }

        /// <summary>
        /// Получить стомость использования машины по vin
        /// </summary>
        /// <param name="info">запрос</param>
        /// <returns>стоисоть</returns>
        /// <exception cref="Exception"></exception>
        [HttpPost(nameof(GetCarUsingCostByVin))]
        public Task<decimal> GetCarUsingCostByVin(GetCarUsingCostRequest info)
        {
            return _carManagerService.GetCarUsingCostByVin(info);
        }

        /// <summary>
        /// Получить стомость использования машины по номеру
        /// </summary>
        /// <param name="info">запрос</param>
        /// <returns>стоисоть</returns>
        /// <exception cref="Exception"></exception>
        [HttpPost(nameof(GetCarUsingCostByLicensePlate))]
        public Task<decimal> GetCarUsingCostByLicensePlate(GetCarUsingCostRequest info)
        {
            return _carManagerService.GetCarUsingCostByLicensePlate(info);
        }
    }
}
