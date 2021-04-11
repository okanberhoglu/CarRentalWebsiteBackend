using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        IResult Add(CarImage entity, IFormFile formFile);
        IResult Delete(CarImage entity);
        IDataResult<List<CarImage>> GetAll();
        IDataResult<CarImage> GetById(int id);
        IResult Update(CarImage entity, IFormFile formFile);
        IDataResult<List<CarImage>> GetByCarId(int id);
    }
}
