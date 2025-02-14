﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.Abstract
{
    public interface IEntityRepositoryBase<T> where T : class, IEntity, new()
    {
        Task<bool> AsyncAddDB(T entity);
        Task<bool> AsyncUpdateDB(T entity);
        Task<bool> AsyncDeleteDB(T entity);
        Task<List<T>> AsyncGetAllDB(Expression<Func<T, bool>> filter = null);
        Task<T> AsyncGetDB(Expression<Func<T, bool>> filter);
        IList<T> GetList(Expression<Func<T, bool>> filter = null);
    }
}
