using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity,TKey> where TEntity:BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool TrackChanges=false);
        Task<TEntity?> GetByIdAsync(TKey Id);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        #region For Specifications
        Task<IEnumerable<TEntity>> GetAllWithSpecificationAsync(Specifications<TEntity> specifications);
        Task<TEntity?> GetByIdWithSpecificationAsync(Specifications<TEntity> specifications);
        #endregion
        Task<int>CountAsync(Specifications<TEntity> specifications)
    }
}
