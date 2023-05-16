﻿using Core.DataAccess.Abstract;
using Core.Entities;
using Core.Entities.Concrete;
using Neo4jClient;
using System.Linq.Expressions;

namespace Core.DataAccess.Concrete.EntityFramework
{
    public class NeoGenericRepositoryBase<TEntity> : IEntityRepositoryBase<TEntity>
        where TEntity : BaseEntity, new()
    {
        private readonly IGraphClient _graphClient;

        public NeoGenericRepositoryBase(IGraphClient graphClient)
        {
            _graphClient = graphClient;
        }

        public Task<bool> AsyncAddDB(TEntity entity)
        {
            return Task.Run(() => { return AddDB(entity); });
        }

        private bool AddDB(TEntity entity)
        {
            try
            {
                string type = typeof(TEntity).Name;
                _graphClient.Cypher.Create("(e:" + type + " $entity)")
                    .WithParam("entity", entity)
                    .ExecuteWithoutResultsAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Task<bool> AsyncDeleteDB(TEntity entity)
        {
            return Task.Run(() => { return DeleteDB(entity); });
        }

        private bool DeleteDB(TEntity entity)
        {
            try
            {
                string type = typeof(TEntity).Name;
                //var property = "e." + type + "Id";               
                var entityId = Convert.ToInt32(entity.GetType().GetProperty("Id").GetValue(entity, null));

                _graphClient.Cypher.Match("(e:" + type + ")")
                    .Where((TEntity e) => e.Id == entityId)
                    .Delete("e")
                    .ExecuteWithoutResultsAsync();
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Task<List<TEntity>> AsyncGetAllDB(Expression<Func<TEntity, bool>> filter = null)
        {
            return Task.Run(() => { return GetAllDB(filter); });
        }

        private List<TEntity> GetAllDB(Expression<Func<TEntity, bool>> filter)
        {
            var type = typeof(TEntity).Name;

            var products = _graphClient.Cypher.Match("(p: " + type + ")")
                        .Return(p => p.As<TEntity>())
                        .OrderBy("1")
                        .ResultsAsync.Result;

            return products.ToList();
        }

        public Task<TEntity> AsyncGetDB(Expression<Func<TEntity, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AsyncUpdateDB(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public IList<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            string entityType = filter.GetType().Name;

            var products = _graphClient.Cypher.Match("(p: " + entityType + ")")
                        .Return(p => p.As<TEntity>())
                        .OrderBy("1")
                        .ResultsAsync.Result;

            return products.ToList();
        }
    }
}
