using _1p_atom_carmanager.backend.core.Abstractions;
using _1p_atom_carmanager.backend.core.Entities;
using _1p_atom_carmanager.backend.core.Requests;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace _1p_atom_carmanager.backend.core.Services
{
    public class CarManagerService : ICarManagerService
    {
        private ContextDb _contextDb;
        private readonly IMapper _mapper;
        public CarManagerService(ContextDb contextDb, IMapper mapper)
        {
            _contextDb = contextDb;
            _mapper = mapper;
        }

        public async Task<List<Car>> GetAllCars()
        {
            List<Car> cars = await _contextDb.Cars.Include(x => x.CarType).Include(x => x.TachographInformation).ToListAsync();
            return cars;
        }

        public async Task<Car> GetCarByLicensePlate(string licensePlate)
        {
            Car car = await _contextDb.Cars.Where(x => x.LicensePlate == licensePlate)
                .Include(x => x.CarType).Include(x => x.TachographInformation).FirstOrDefaultAsync();
            return car;
        }

        public async Task<Car> GetCarByVin(string vin)
        {
            Car car = await _contextDb.Cars.Where(x => x.VehicleIdentificationNumber == vin)
                .Include(x => x.CarType).Include(x => x.TachographInformation).FirstOrDefaultAsync();
            return car;
        }

        public async Task<string> RegNewCar(RegNewCarRequest carRequest)
        {
            if (carRequest == null)
                throw new Exception($"Object car is null");
            carRequest.Year = carRequest.Year.Date;
            TachographInfo tachographInfo = new TachographInfo();
            tachographInfo.Id = Guid.NewGuid();
            tachographInfo.VehicleSpeed = carRequest.VehicleSpeedFromTachograph;
            tachographInfo.DistanceTraveled = carRequest.DistanceTraveled;
            tachographInfo.Rpm = carRequest.Rpm;
            await _contextDb.TachographInfos.AddAsync(tachographInfo);
            await _contextDb.SaveChangesAsync();
            CarType carType = await _contextDb.CarTypes.Where(x => x.Title == carRequest.CarType).FirstOrDefaultAsync();
            if (carType == null)
                throw new Exception("Null element Car Type");
            var car = _mapper.Map<Car>(carRequest);
            car.CarType = carType;
            car.TachographInformation = tachographInfo;
            await _contextDb.Cars.AddAsync(car);
            await _contextDb.SaveChangesAsync();
            return "Successfuly registration car";
        }

        public async Task<string> RemoveCarByVin(string vin)
        {
            if (string.IsNullOrWhiteSpace(vin))
                throw new Exception("недопустимое значение vin");
            Car car = await _contextDb.Cars.Where(x => x.VehicleIdentificationNumber == vin).FirstOrDefaultAsync();
            if (car == null)
                throw new Exception($"Could not find car by {vin}");
            _contextDb.Cars.Remove(car);
            await _contextDb.SaveChangesAsync();
            return "Successfuly removed";
        }

        public async Task<string> UpdateCarInfoByLicensePlate(UpdateCartInfoByLicensePlate info)
        {
            if (info == null)
                throw new Exception("Null update info");
            Guid id = await _contextDb.Cars.Where(x => x.LicensePlate == info.LicensePlate)
                .Select(x => x.Id).FirstOrDefaultAsync();
            if (id == Guid.Empty)
                throw new Exception($"Null element LicensePlate");
            info.Year = info.Year.Date;
            TachographInfo tachographInfo = await _contextDb.TachographInfos.Where(x => x.Id == info.TachographId).FirstOrDefaultAsync();
            if (tachographInfo == null)
                throw new Exception("Not info by Tachograph Info");
            tachographInfo.VehicleSpeed = info.VehicleSpeedFromTachograph;
            tachographInfo.DistanceTraveled = info.DistanceTraveled;
            tachographInfo.Rpm = info.Rpm;
            _contextDb.TachographInfos.Update(tachographInfo);
            CarType carType = await _contextDb.CarTypes.Where(x => x.Title == info.CarType).FirstOrDefaultAsync();
            if (carType == null)
                throw new Exception("Null element Car Type");
            var car = _mapper.Map<Car>(info);
            car.Id = id;
            car.CarType = carType;
            car.TachographInformation = tachographInfo;
            //car = null;
            //car.Id = id;
            _contextDb.Cars.Update(car);
            await _contextDb.SaveChangesAsync();
            return "Successfuly updated";
        }

        public async Task<string> UpdateCarInfoByVin(UpdateCartInfoByVin info)
        {
            if (info == null)
                throw new Exception("Null update info");
            Guid id = await _contextDb.Cars.Where(x => x.VehicleIdentificationNumber == info.VehicleIdentificationNumber)
                .Select(x => x.Id).FirstOrDefaultAsync();
            if (id == Guid.Empty)
                throw new Exception($"Null element LicensePlate");
            info.Year = info.Year.Date;
            TachographInfo tachographInfo = await _contextDb.TachographInfos.Where(x => x.Id == info.TachographId).FirstOrDefaultAsync();
            if (tachographInfo == null)
                throw new Exception("Not info by Tachograph Info");
            tachographInfo.VehicleSpeed = info.VehicleSpeedFromTachograph;
            tachographInfo.DistanceTraveled = info.DistanceTraveled;
            tachographInfo.Rpm = info.Rpm;
            _contextDb.TachographInfos.Update(tachographInfo);
            CarType carType = await _contextDb.CarTypes.Where(x => x.Title == info.CarType).FirstOrDefaultAsync();
            if (carType == null)
                throw new Exception("Null element Car Type");
            var car = _mapper.Map<Car>(info);
            car.Id = id;
            car.CarType = carType;
            car.TachographInformation = tachographInfo;
            //car = null;
            //car.Id = id;
            _contextDb.Cars.Update(car);
            await _contextDb.SaveChangesAsync();
            return "Successfuly updated";
        }

        public async Task<string> AddCarType(string type)
        {
            if (type == string.Empty)
                throw new Exception("Null element car type");
            CarType carType = new CarType();
            carType.Title = type;
            carType.Id = Guid.NewGuid();
            await _contextDb.CarTypes.AddAsync(carType);
            await _contextDb.SaveChangesAsync();
            return "Successfuly added";
        }

        public async Task<List<CarType>> GetCarTypes()
        {
            return await _contextDb.CarTypes.ToListAsync();
        }

        public async Task<decimal> GetCarUsingCostByVin(GetCarUsingCostRequest info)
        {
            decimal finesCost = info.fines == null ? 0 : info.fines.Select(x => x.Cost).Sum();
            Car car = await _contextDb.Cars.Where(x => x.VehicleIdentificationNumber == info.number).FirstOrDefaultAsync();
            if (car == null)
                throw new Exception($"Could not find car by {info.number}");
            decimal fuelAllCost = info.fuelCost * Convert.ToDecimal(car.TotalFuelUsed);
            decimal result = finesCost + fuelAllCost;
            return result;
        }

        public async Task<decimal> GetCarUsingCostByLicensePlate(GetCarUsingCostRequest info)
        {
            decimal finesCost = info.fines == null ? 0 : info.fines.Select(x => x.Cost).Sum();
            Car car = await _contextDb.Cars.Where(x => x.LicensePlate == info.number).FirstOrDefaultAsync();
            if (car == null)
                throw new Exception($"Could not find car by {info.number}");
            decimal fuelAllCost = info.fuelCost * Convert.ToDecimal(car.TotalFuelUsed);
            decimal result = finesCost + fuelAllCost;
            return result;
        }
    }
}
