using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TCC.GestaoSaude.DataAccess.Interface
{
    public interface IRepositorio<T> where T : class
    {
        T Add(T t);
        Task<T> AddAsyn(T t);
        int Count();
        Task<int> CountAsync();
        void Delete(T entity);
        Task<int> DeleteAsyn(T entity);
        void Dispose();
        T Find(Expression<Func<T, bool>> match);
        T Find(Expression<Func<T, bool>> match, List<string> includes);
        ICollection<T> FindAll(Expression<Func<T, bool>> match);
        ICollection<T> FindAll(Expression<Func<T, bool>> match, List<string> includes);
        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match);
        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match, List<string> includes);
        Task<T> FindAsync(Expression<Func<T, bool>> match);
        Task<T> FindAsync(Expression<Func<T, bool>> match, List<string> includes);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        Task<ICollection<T>> FindByAsyn(Expression<Func<T, bool>> predicate);
        T Get(int id);
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(List<string> includes);
        Task<ICollection<T>> GetAllAsyn();
        Task<ICollection<T>> GetAllAsyn(List<string> includes);
        Task<T> GetAsync(int id);
        void Save();
        Task<int> SaveAsync();
        T Update(T t, object key);
        Task<T> UpdateAsyn(T t, object key);
    }
}
