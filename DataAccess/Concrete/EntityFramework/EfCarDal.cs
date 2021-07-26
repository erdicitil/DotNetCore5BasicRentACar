using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, RentACarContext>, ICarDal
    {
        public List<CarDetailsDTO> GetCarDetails(Expression<Func<CarDetailsDTO, bool>> filter = null)
        {
            using (RentACarContext context = new RentACarContext())
            {
                var result = from car in context.Cars
                    join brand in context.Brands
                        on car.BrandId equals brand.BrandId
                    join color in context.Colors
                        on car.ColorId equals color.ColorId
                    select new CarDetailsDTO
                    {
                        Id = car.CarId,
                        BrandId = car.BrandId,
                        ColorId = car.ColorId,
                        CarName = car.CarName,
                        BrandName = brand.BrandName,
                        ColorName = color.ColorName,
                        DailyPrice = car.DailyPrice,
                        ModelYear = car.ModelYear,
                        Description = car.Description,
                        CarImage = (from i in context.CarImages
                            where (car.CarId == i.CarId)
                            select i.ImagePath).FirstOrDefault()


                    };

                return filter == null ? result.ToList() : result.Where(filter).ToList();
            }
        }


    }
}
