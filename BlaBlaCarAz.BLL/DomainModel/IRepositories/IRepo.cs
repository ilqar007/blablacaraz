using BlaBlaCarAz.BLL.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlaBlaCarAz.BLL.DomainModel.IRepositories
{
    public interface IRepo<T> : IDisposable where T : EntityBase
    {
        Task<T> GetSingleAsync(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition);
        Task<IList<T>> GetAllAsync(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition);
        Task<IList<T>> GetAllAsync(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition, int pageNumber, int pageSize, System.Linq.Expressions.Expression<Func<T, object>> orderByExpression);
        Task<IList<T>> GetAllAsync(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition, System.Linq.Expressions.Expression<Func<T, object>> orderByExpression);

        Task<IList<T>> QueryAsync(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition);
        Task<long> CountAsync(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition);
        Task<int> CountAsync();
        bool HasChanges { get; }
        Task<T> FindAsync(long? id);
        Task<T> GetFirstAsync();
        Task<T> GetLastAsync();
        Task<IList<T>> GetAllAsync();
        IEnumerable<T> GetRange(int skip, int take);
        Task<int> AddAsync(T entity, bool persist = true);
        Task<int> AddRangeAsync(IEnumerable<T> entities, bool persist = true);
        Task<int> UpdateAsync(T entity, bool persist = true);
        Task<int> UpdateRangeAsync(IEnumerable<T> entities, bool persist = true);
        Task<int> DeleteAsync(T entity, bool persist = true);
        Task<int> DeleteRangeAsync(IEnumerable<T> entities, bool persist = true);
        Task<int> DeleteAsync(int id, byte[] timeStamp, bool persist = true);
        Task<int> SaveChangesAsync();

    }

}
