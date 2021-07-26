using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IRentalService
    {
        IDataResult<List<Rental>> GetAll();
        IDataResult<Rental> GetById(int rentalId);
        IDataResult<List<Rental>> GetByRentDate(DateTime min, DateTime max);
        IDataResult<List<Rental>> GetByReturnDate(DateTime min, DateTime max);
        IResult Add(Rental rental);
        IResult Update(Rental rental);
        IResult Delete(Rental rental);

    }
}
