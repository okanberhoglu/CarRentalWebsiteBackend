using Core.Entities;
using Core.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Business
{
    public interface IService<T> where T: class,IEntity,new()
    {
        IDataResult<List<T>> GetAll();
        IDataResult<T> GetById(int id);
        IResult Add(T entity);
        IResult Update(T entity);
        IResult Delete(T entity);
    }
}
