﻿using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserOperationClaimService : IGenericService<UserOperationClaim>
    {
        IDataResult<List<UserOperationClaimDetail>> GetDetail();
        IDataResult<List<UserOperationClaimDetail>> GetDetailById(int userOperationId);
    }
}
