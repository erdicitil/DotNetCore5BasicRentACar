using Business.Abstract;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.BusinessAspects.Autofac;
using Business.Constans;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        IBrandService _brandService;
        IColorService _colorService;

        public CarManager(ICarDal carDal, IBrandService brandService, IColorService colorService)
        {
            _carDal = carDal;
            _brandService = brandService;
            _colorService = colorService;
        }

        [SecuredOperation("admin,mod")]
        [ValidationAspect(typeof(CarValidator))]
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect("ICarService.GetAll")]
        public IResult Add(Car car)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            _carDal.Add(car);
            return new SuccessResult(Messages.CarAdded);
        }

        [SecuredOperation("admin,mod")]
        [ValidationAspect(typeof(CarValidator))]
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect("ICarService.GetAll")]
        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.CarDeleted);
        }

        public IDataResult<List<Car>> GetAll()
        {
            if (DateTime.Now.Hour == 00)
            {
                return new ErrorDataResult<List<Car>>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.CarListed);
        }

        public IDataResult<List<Car>> GetAllByBrandId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(car => car.BrandId == id));
        }

        public IDataResult<List<Car>> GetAllByColorId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(car => car.ColorId == id));
        }

        public IDataResult<List<Car>> GetByDailyPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Car>>(
                _carDal.GetAll(car => car.DailyPrice >= min && car.DailyPrice <= max));
        }

        public IDataResult<Car> GetById(int carId)
        {
            return new SuccessDataResult<Car>(_carDal.Get(car=>car.CarId == carId));
        }

        public IDataResult<List<Car>> GetByModelYear(int min, int max)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(car =>
                car.ModelYear >= min && car.ModelYear <= max));
        }

        public IDataResult<List<CarDetailsDTO>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailsDTO>>(_carDal.GetCarDetails());
        }

        public IDataResult<List<CarDetailsDTO>> GetCarDetailsById(int carId)
        {
            return new SuccessDataResult<List<CarDetailsDTO>>(_carDal.GetCarDetails(car => car.Id==carId));
        }

        [SecuredOperation("admin,mod")]
        [ValidationAspect(typeof(CarValidator))]
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect("ICarService.GetAll")]
        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult(Messages.CarUptaded);
        }

    }
}
