using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OnlineTestForCheckingKnowledge.Data;
using OnlineTestForCheckingKnowledge.Data.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace OnlineTestForCheckingKnowledge.Infrastructure.Repositories
{
    public interface IRepository<T> where T : class, IBaseEntity 
    {
        Task<T> GetByIdAsync(int id); 
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
        Task<int> SaveChangesAsync(); 
    }
}