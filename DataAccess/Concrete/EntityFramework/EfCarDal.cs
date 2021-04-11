
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using Autofac.Features.ResolveAnything;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, CarRentalContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails()
        {
            using (CarRentalContext context = new CarRentalContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands
                             on c.BrandId equals b.BrandId
                             join co in context.Colors
                             on c.ColorId equals co.ColorId
                             select new CarDetailDto
                             {
                                 ModelYear = c.ModelYear, BrandName = b.BrandName, CarId = c.CarId, DailyPrice = c.DailyPrice, ColorName = co.ColorName, Description = c.Description, BrandId = c.BrandId, ColorId = c.ColorId
                             };
                return result.ToList();
            }
            
        }

        public List<CarDetailDto> GetCarsDetails(Expression<Func<CarDetailDto, bool>> filter = null)
        {
            using (CarRentalContext context = new CarRentalContext() )
            {
                var result = from c in context.Cars
                    join b in context.Brands
                        on c.BrandId equals b.BrandId
                    join co in context.Colors
                        on c.ColorId equals co.ColorId
                    select new CarDetailDto
                    {
                        ModelYear = c.ModelYear, BrandId = b.BrandId, ColorId = co.ColorId, BrandName = b.BrandName, CarId = c.CarId, ColorName = co.ColorName, DailyPrice = c.DailyPrice, Description = c.Description
                    };
                return filter == null
                    ? result.ToList()
                    : result.Where(filter).ToList();
            }
        }
    }
}
