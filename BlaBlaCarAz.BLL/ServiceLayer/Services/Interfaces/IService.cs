using BlaBlaCarAz.BLL.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlaBlaCarAz.BLL.ServiceLayer.Services.Interfaces
{
    public interface IService<T> where T : EntityBase
    {
        ///// <summary>
        ///// Get a selected extiry by the object primary key ID
        ///// </summary>
        ///// <param name="id">Primary key ID</param>
        //T GetSingle(Expression<Func<T, bool>> whereCondition);

        ///// <summary> 
        ///// Add entity to the repository 
        ///// </summary> 
        ///// <param name="entity">the entity to add</param> 
        ///// <returns>The added entity</returns> 
        //void Add(T entity);

        ///// <summary> 
        ///// Mark entity to be deleted within the repository 
        ///// </summary> 
        ///// <param name="entity">The entity to delete</param> 
        //void Delete(T entity);

        ///// <summary> 
        ///// Updates entity within the the repository 
        ///// </summary> 
        ///// <param name="entity">the entity to update</param> 
        ///// <returns>The updates entity</returns> 
        //void Update(T entity);

        ///// <summary> 
        ///// Load the entities using a linq expression filter
        ///// </summary> 
        ///// <typeparam name="E">the entity type to load</typeparam> 
        ///// <param name="where">where condition</param> 
        ///// <returns>the loaded entity</returns> 
        //IList<T> GetAll(Expression<Func<T, bool>> whereCondition);

        ///// <summary>
        ///// Get all the element of this repository
        ///// </summary>
        ///// <returns></returns>
        //IList<T> GetAll();

        ///// <summary> 
        ///// Query entities from the repository that match the linq expression selection criteria
        ///// </summary> 
        ///// <typeparam name="E">the entity type to load</typeparam> 
        ///// <param name="where">where condition</param> 
        ///// <returns>the loaded entity</returns> 
        //IQueryable<T> Query(Expression<Func<T, bool>> whereCondition);

        ///// <summary>
        ///// Count using a filer
        ///// </summary>
        //long Count(Expression<Func<T, bool>> whereCondition);

        ///// <summary>
        ///// All item count
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <returns></returns>
        //long Count();




        Task<T> GetSingleAsync(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition);
        Task<IList<T>> GetAllAsync(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition);
        Task<IList<T>> GetAllAsync(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition, int pageNumber, int pageSize, Expression<Func<T, object>> orderByExpression);

        Task<IList<T>> GetAllAsync(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition, Expression<Func<T, object>> orderByExpression);

        IQueryable<T> Query(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition);
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
