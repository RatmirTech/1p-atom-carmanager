using _1p_atom_carmanager.backend.core.Abstractions;
using _1p_atom_carmanager.backend.core.Entities;
using _1p_atom_carmanager.backend.core.Requests;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace _1p_atom_carmanager.backend.core.Services
{
    /// <summary>
    /// Сервис по работе с машинами
    /// </summary>
    public class CarManagerService : ICarManagerService
    {
        /// <summary>
        /// бд контекст
        /// </summary>
        private ContextDb _contextDb;
        /// <summary>
        /// Маппер
        /// </summary>
        private readonly IMapper _mapper;
        /// <summary>
        /// Логгер
        /// </summary>
        private readonly ILogger<CarManagerService> _logger;
        /// <summary>
        /// Конструткор сервиса
        /// </summary>
        /// <param name="contextDb">бд контекст</param>
        /// <param name="mapper">маппер</param>
        /// <param name="logger">логгер</param>
        public CarManagerService(ContextDb contextDb, IMapper mapper, ILogger<CarManagerService> logger)
        {
            _contextDb = contextDb;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Получить все машины
        /// </summary>
        /// <returns></returns>
        public async Task<List<Car>> GetAllCars()
        {
            List<Car> cars = await _contextDb.Cars.Include(x => x.CarType).Include(x => x.TachographInformation).ToListAsync();
            return cars;
        }

        /// <summary>
        /// Получить машину по номеру
        /// </summary>
        /// <param name="licensePlate">номер</param>
        /// <returns>объект машины</returns>
        public async Task<Car> GetCarByLicensePlate(string licensePlate)
        {
            Car car = await _contextDb.Cars.Where(x => x.LicensePlate == licensePlate)
                .Include(x => x.CarType).Include(x => x.TachographInformation).FirstOrDefaultAsync();
            if (car == null)
            {
                _logger.LogError($"Could not find car by license plate {licensePlate}");
                throw new Exception($"Could not find car by license plate {licensePlate}");
            }
            return car;
        }

        /// <summary>
        /// Получить машину по VIN
        /// </summary>
        /// <param name="vin">vin</param>
        /// <returns>объект машины</returns>
        public async Task<Car> GetCarByVin(string vin)
        {
            Car car = await _contextDb.Cars.Where(x => x.VehicleIdentificationNumber == vin)
                .Include(x => x.CarType).Include(x => x.TachographInformation).FirstOrDefaultAsync();
            return car;
        }

        /// <summary>
        /// Зарегистрировать новую машину
        /// </summary>
        /// <param name="carRequest">запрос</param>
        /// <returns>результат</returns>
        /// <exception cref="Exception"></exception>
        public async Task<string> RegNewCar(RegNewCarRequest carRequest)
        {
            if (carRequest == null)
            {
                _logger.LogError($"Object car is null");
                throw new Exception($"Object car is null");
            }
            carRequest.Year = carRequest.Year.Date;

            //Объект тахографа и сразу записываем в бд
            TachographInfo tachographInfo = new TachographInfo();
            tachographInfo.Id = Guid.NewGuid();
            tachographInfo.VehicleSpeed = carRequest.VehicleSpeedFromTachograph;
            tachographInfo.DistanceTraveled = carRequest.DistanceTraveled;
            tachographInfo.Rpm = carRequest.Rpm;
            await _contextDb.TachographInfos.AddAsync(tachographInfo);
            await _contextDb.SaveChangesAsync();

            //Ищем тип машинки
            CarType carType = await _contextDb.CarTypes.Where(x => x.Title == carRequest.CarType).FirstOrDefaultAsync();
            if (carType == null)
            {
                _logger.LogError("Null element Car Type");
                throw new Exception("Null element Car Type");
            }

            //маппим свойства с dto в объект машинки
            var car = _mapper.Map<Car>(carRequest);
            car.CarType = carType;
            car.TachographInformation = tachographInfo;
            await _contextDb.Cars.AddAsync(car);
            await _contextDb.SaveChangesAsync();
            return "Successfuly registration car";
        }

        /// <summary>
        /// Удалить машину по vin
        /// </summary>
        /// <param name="vin">vin</param>
        /// <returns>результат</returns>
        /// <exception cref="Exception"></exception>
        public async Task<string> RemoveCarByVin(string vin)
        {
            if (string.IsNullOrWhiteSpace(vin))
            {
                _logger.LogError($"недопустимое значение vin {vin}");
                throw new Exception("недопустимое значение vin");
            }
            Car car = await _contextDb.Cars.Where(x => x.VehicleIdentificationNumber == vin).FirstOrDefaultAsync();
            if (car == null)
            {
                _logger.LogError($"Could not find car by vin {vin}");
                throw new Exception($"Could not find car by vin {vin}");
            }

            _contextDb.Cars.Remove(car);
            await _contextDb.SaveChangesAsync();
            return "Successfuly removed";
        }

        /// <summary>
        /// Обновить информацию о машине по номеру
        /// </summary>
        /// <param name="info">запрос</param>
        /// <returns>результат</returns>
        /// <exception cref="Exception"></exception>
        public async Task<string> UpdateCarInfoByLicensePlate(UpdateCartInfoByLicensePlate info)
        {
            if (info == null)
            {
                _logger.LogError("Null update info");
                throw new Exception("Null update info");
            }

            //получаем айди машинки с нашим номером
            Guid id = await _contextDb.Cars.Where(x => x.LicensePlate == info.LicensePlate)
                .Select(x => x.Id).FirstOrDefaultAsync();
            if (id == Guid.Empty)
            {
                _logger.LogError($"Null element by license plate {info.LicensePlate}");
                throw new Exception($"Null element by license plate {info.LicensePlate}");
            }

            info.Year = info.Year.Date;
            //Получаем тахограф и обновляем инфу
            TachographInfo tachographInfo = await _contextDb.TachographInfos.Where(x => x.Id == info.TachographId).FirstOrDefaultAsync();
            if (tachographInfo == null)
            {
                _logger.LogError("Not info by Tachograph Info");
                throw new Exception("Not info by Tachograph Info");
            }
            tachographInfo.VehicleSpeed = info.VehicleSpeedFromTachograph;
            tachographInfo.DistanceTraveled = info.DistanceTraveled;
            tachographInfo.Rpm = info.Rpm;
            _contextDb.TachographInfos.Update(tachographInfo);

            //Тип машинки получаем
            CarType carType = await _contextDb.CarTypes.Where(x => x.Title == info.CarType).FirstOrDefaultAsync();
            if (carType == null)
            {
                _logger.LogError("Null element Car Type");
                throw new Exception("Null element Car Type");
            }

            //маппим с dto в машинку и сохраняем в бд
            var car = _mapper.Map<Car>(info);
            car.Id = id;
            car.CarType = carType;
            car.TachographInformation = tachographInfo;
            _contextDb.Cars.Update(car);
            await _contextDb.SaveChangesAsync();
            return "Successfuly updated";
        }

        /// <summary>
        /// Обновить информацию о машине по vin
        /// </summary>
        /// <param name="info">запрос</param>
        /// <returns>результат</returns>
        /// <exception cref="Exception"></exception>
        public async Task<string> UpdateCarInfoByVin(UpdateCartInfoByVin info)
        {
            if (info == null)
            {
                _logger.LogError("Null update info");
                throw new Exception("Null update info");
            }

            //получаем айди машинки с нашим вином
            Guid id = await _contextDb.Cars.Where(x => x.VehicleIdentificationNumber == info.VehicleIdentificationNumber)
                .Select(x => x.Id).FirstOrDefaultAsync();
            if (id == Guid.Empty)
            {
                _logger.LogError($"Null element by vin {info.VehicleIdentificationNumber}");
                throw new Exception($"Null element by vin {info.VehicleIdentificationNumber}");
            }

            info.Year = info.Year.Date;

            //Получаем тахограф и обновляем инфу
            TachographInfo tachographInfo = await _contextDb.TachographInfos.Where(x => x.Id == info.TachographId).FirstOrDefaultAsync();
            if (tachographInfo == null)
            {
                _logger.LogError("Not info by Tachograph Info");
                throw new Exception("Not info by Tachograph Info");
            }
            tachographInfo.VehicleSpeed = info.VehicleSpeedFromTachograph;
            tachographInfo.DistanceTraveled = info.DistanceTraveled;
            tachographInfo.Rpm = info.Rpm;
            _contextDb.TachographInfos.Update(tachographInfo);

            //Тип машинки получаем
            CarType carType = await _contextDb.CarTypes.Where(x => x.Title == info.CarType).FirstOrDefaultAsync();
            if (carType == null)
            {
                _logger.LogError("Null element Car Type");
                throw new Exception("Null element Car Type");
            }

            //маппим с dto в машинку и сохраняем в бд
            var car = _mapper.Map<Car>(info);
            car.Id = id;
            car.CarType = carType;
            car.TachographInformation = tachographInfo;
            _contextDb.Cars.Update(car);
            await _contextDb.SaveChangesAsync();
            return "Successfuly updated";
        }

        /// <summary>
        /// Добавить тип машины
        /// </summary>
        /// <param name="type">Тип</param>
        /// <returns>результат</returns>
        /// <exception cref="Exception"></exception>
        public async Task<string> AddCarType(string type)
        {
            if (type == string.Empty)
            {
                _logger.LogError("Null element car type");
                throw new Exception("Null element car type");
            }

            //Создаём объект типа машинки и сохраняем в бд
            CarType carType = new CarType();
            carType.Title = type;
            carType.Id = Guid.NewGuid();
            await _contextDb.CarTypes.AddAsync(carType);
            await _contextDb.SaveChangesAsync();
            return "Successfuly added";
        }

        /// <summary>
        /// Получить все типы машин
        /// </summary>
        /// <returns>список типов машин</returns>
        public async Task<List<CarType>> GetCarTypes()
        {
            return await _contextDb.CarTypes.ToListAsync();
        }

        /// <summary>
        /// Получить стомость использования машины по vin
        /// </summary>
        /// <param name="info">запрос</param>
        /// <returns>стоисоть</returns>
        /// <exception cref="Exception"></exception>
        public async Task<decimal> GetCarUsingCostByVin(GetCarUsingCostRequest info)
        {
            //Считаем стоимость всех штрафоф и услуг
            decimal finesCost = info.fines == null ? 0 : info.fines.Select(x => x.Cost).Sum();

            //Получаем машинку по vin
            Car car = await _contextDb.Cars.Where(x => x.VehicleIdentificationNumber == info.number).FirstOrDefaultAsync();
            if (car == null)
            {
                _logger.LogError($"Could not find car by {info.number}");
                throw new Exception($"Could not find car by {info.number}");
            }

            //Считаем стоимость топлива
            decimal fuelAllCost = info.fuelCost * Convert.ToDecimal(car.TotalFuelUsed);

            //складываем все услуги
            decimal result = finesCost + fuelAllCost;
            return result;
        }

        /// <summary>
        /// Получить стомость использования машины по номеру
        /// </summary>
        /// <param name="info">запрос</param>
        /// <returns>стоисоть</returns>
        /// <exception cref="Exception"></exception>
        public async Task<decimal> GetCarUsingCostByLicensePlate(GetCarUsingCostRequest info)
        {
            //Считаем стоимость всех штрафоф и услуг
            decimal finesCost = info.fines == null ? 0 : info.fines.Select(x => x.Cost).Sum();

            //Получаем машинку по vin
            Car car = await _contextDb.Cars.Where(x => x.VehicleIdentificationNumber == info.number).FirstOrDefaultAsync();
            if (car == null)
            {
                _logger.LogError($"Could not find car by {info.number}");
                throw new Exception($"Could not find car by {info.number}");
            }

            //Считаем стоимость топлива
            decimal fuelAllCost = info.fuelCost * Convert.ToDecimal(car.TotalFuelUsed);

            //складываем все услуги
            decimal result = finesCost + fuelAllCost;
            return result;
        }
    }
}
