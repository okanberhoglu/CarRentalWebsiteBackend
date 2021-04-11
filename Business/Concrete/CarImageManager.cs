using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Helpers;
using System.Linq;
using System.IO;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;
        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }
        public IResult Add(CarImage entity, IFormFile formFile)
        {
            var result = BusinessRules.Run(CheckIfImageLimitExceed(entity.CarId));
            if (result != null)
            {
                return result;
            }
            entity.ImagePath = FileHelper.Add(formFile);
            entity.Date = DateTime.Now;
            _carImageDal.Add(entity);
            return new SuccessResult(Messages.ImageAdded);
        }

        public IResult Delete(CarImage entity)
        {
            var oldpath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\wwwroot")) + _carImageDal.Get(p => p.Id == entity.Id).ImagePath;
            IResult result = BusinessRules.Run(FileHelper.Delete(oldpath));

            if (result != null)
            {
                return result;
            }

            _carImageDal.Delete(entity);
            return new SuccessResult(Messages.ImageDeleted);
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }

        public IDataResult<CarImage> GetById(int id)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(i => i.Id == id));
        }
        public IDataResult<List<CarImage>> GetByCarId(int id)
        {
            IResult result = BusinessRules.Run(CheckIfCarImageNull(id));

            if (result != null)
            {
                return new ErrorDataResult<List<CarImage>>(result.Message);
            }

            return new SuccessDataResult<List<CarImage>>(CheckIfCarImageNull(id).Data);
        }

        public IResult Update(CarImage entity, IFormFile formFile)
        {
            var oldPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\wwwroot")) + _carImageDal.Get(p => p.Id == entity.CarId).ImagePath;

            entity.ImagePath = FileHelper.Update(oldPath, formFile);
            entity.Date = DateTime.Now;
            _carImageDal.Update(entity);
            return new SuccessResult();
        }

        private IResult CheckIfImageLimitExceed(int id)
        {
            var result = _carImageDal.GetAll(i => i.CarId == id).Count;
            if (result > 5)
            {
                return new ErrorResult(Messages.ImageLimitExceed);
            }
            return new SuccessResult();
        }
        private IDataResult<List<CarImage>> CheckIfCarImageNull(int id)
        {
            try
            {
                string path = @"\Images\Default.jpg";
                var result = _carImageDal.GetAll(c => c.CarId == id).Any();
                if (!result)
                {
                    List<CarImage> carImage = new List<CarImage>();
                    carImage.Add(new CarImage { CarId = id, ImagePath = path, Date = DateTime.Now});
                    return new SuccessDataResult<List<CarImage>>(carImage);
                }
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<List<CarImage>>(exception.Message);
            }
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(c => c.CarId == id));
        }
    }
}
