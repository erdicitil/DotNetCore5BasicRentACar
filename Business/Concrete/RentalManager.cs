using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Constans;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class RentalManager:IRentalService
    {
        private IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        public IDataResult<List<Rental>> GetAll()
        {
            if (DateTime.Now.Hour == 00)
            {
                return new ErrorDataResult<List<Rental>>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(), Messages.RentalListed);
        }

        public IDataResult<Rental> GetById(int rentalId)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(rental => rental.RentalId == rentalId));
        }

        public IDataResult<List<Rental>> GetByRentDate(DateTime min, DateTime max)
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(rental =>
                rental.RentDate >= min && rental.RentDate <= max));
        }

        public IDataResult<List<Rental>> GetByReturnDate(DateTime min, DateTime max)
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(rental =>
                rental.ReturnDate >= min && rental.ReturnDate <= max));
        }

        public IResult Add(Rental rental)
        {
            IResult result = BusinessRules.Run();
            if (result != null)
            {
                return result;
            }
            _rentalDal.Add(rental);
            return new SuccessResult(Messages.RentalAdded);
        }

        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult(Messages.RentalUptaded);
        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.RentalDeleted);
        }
    }
}
