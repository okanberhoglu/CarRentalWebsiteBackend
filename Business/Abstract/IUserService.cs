using Core.Utilities.Business;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;

namespace Business.Abstract
{
    public interface IUserService: IService<User>
    {
        IDataResult<List<OperationClaim>> GetClaims(User user);
        IDataResult<User> GetByMail(string email);
    }
}
