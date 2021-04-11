using Core.Utilities.Business;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICarService: IService<Car>
    {
        
        IDataResult<List<Car>> GetByDailyPrice(decimal min, decimal max);
        IDataResult<List<CarDetailDto>> GetCarDetails();

        IResult AddTransactionalTest(Car car);

        IDataResult<List<CarDetailDto>> GetByBrandId(int id);
        IDataResult<List<CarDetailDto>> GetByColorId(int id);


    }
}
