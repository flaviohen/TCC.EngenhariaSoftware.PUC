using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TCC.GestaoSaude.DataAccess.Interface;
using TCC.GestaoSaude.DataAccess.Contexto;

namespace TCC.GestaoSaude.DataAccess.Repositorio
{
	public class Repositorio<TEntity> :
		IRepositorio<TEntity> where TEntity : class
	{
		protected GestaoSaudeContext _context;

		public Repositorio(GestaoSaudeContext context)
		{
			_context = context;
		}

		public IQueryable<TEntity> GetAll()
		{
			return _context.Set<TEntity>();
		}

		public IQueryable<TEntity> GetAll(List<string> includes)
		{
			var query = _context.Set<TEntity>().AsQueryable();

			if (includes != null && includes.Count > 0)
			{
				foreach (var include in includes)
				{
					query = query.Include(include);
				}
			}
			return query;
		}

		public virtual async Task<ICollection<TEntity>> GetAllAsyn()
		{
			return await _context.Set<TEntity>().ToListAsync();
		}

		public virtual async Task<ICollection<TEntity>> GetAllAsyn(List<string> includes)
		{
			var query = _context.Set<TEntity>().AsQueryable();

			if (includes != null && includes.Count > 0)
			{
				foreach (var include in includes)
				{
					query = query.Include(include);
				}
			}
			return await query.ToListAsync();
		}

		public virtual TEntity Get(int id)
		{
			return _context.Set<TEntity>().Find(id);
		}

		public virtual async Task<TEntity> GetAsync(int id)
		{
			return await _context.Set<TEntity>().FindAsync(id);
		}

		public virtual TEntity Add(TEntity t)
		{
			_context.Set<TEntity>().Add(t);
			_context.SaveChanges();
			return t;
		}

		public virtual async Task<TEntity> AddAsyn(TEntity t)
		{
			_context.Set<TEntity>().Add(t);
			await _context.SaveChangesAsync();
			return t;

		}

		public virtual TEntity Find(Expression<Func<TEntity, bool>> match)
		{
			return _context.Set<TEntity>().SingleOrDefault(match);
		}

		public virtual TEntity Find(Expression<Func<TEntity, bool>> match, List<string> includes)
		{
			var query = _context.Set<TEntity>().AsQueryable();
			if (includes != null && includes.Count > 0)
			{
				foreach (var include in includes)
				{
					query = query.Include(include);
				}
			}
			return query.SingleOrDefault(match);
		}
		public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match)
		{
			try
			{
				return await _context.Set<TEntity>().SingleOrDefaultAsync(match);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			
		}

		public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match, List<string> includes)
		{
			var query = _context.Set<TEntity>().AsQueryable();

			if (includes != null && includes.Count > 0)
			{
				foreach (var include in includes)
				{
					query = query.Include(include);
				}
			}
			return await query.SingleOrDefaultAsync(match);
		}

		public ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> match)
		{
			return _context.Set<TEntity>().Where(match).ToList();
		}

		public ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> match, List<string> includes)
		{
			var query = _context.Set<TEntity>().Where(match).AsQueryable();
			if (includes != null && includes.Count > 0)
			{
				foreach (var include in includes)
				{
					query = query.Include(include);
				}
			}

			return query.ToList();
		}

		public async Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match)
		{
			return await _context.Set<TEntity>().Where(match).ToListAsync();
		}
		public async Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match, List<string> includes)
		{
			var query = _context.Set<TEntity>().Where(match).AsQueryable();
			if (includes != null && includes.Count > 0)
			{
				foreach (var include in includes)
				{
					query = query.Include(include);
				}
			}

			return await query.ToListAsync();
		}
		public virtual void Delete(TEntity entity)
		{
			_context.Set<TEntity>().Remove(entity);
			_context.SaveChanges();
		}

		public virtual async Task<int> DeleteAsyn(TEntity entity)
		{
			_context.Set<TEntity>().Remove(entity);
			return await _context.SaveChangesAsync();
		}

		public virtual TEntity Update(TEntity t, object key)
		{
			if (t == null)
				return null;
			TEntity exist = _context.Set<TEntity>().Find(key);
			if (exist != null)
			{
				_context.Entry(exist).CurrentValues.SetValues(t);
				_context.SaveChanges();
			}
			return exist;
		}

		public virtual async Task<TEntity> UpdateAsyn(TEntity t, object key)
		{
			if (t == null)
				return null;
			TEntity exist = await _context.Set<TEntity>().FindAsync(key);
			if (exist != null)
			{
				_context.Entry(exist).CurrentValues.SetValues(t);
				await _context.SaveChangesAsync();
			}
			return exist;
		}

		public int Count()
		{
			return _context.Set<TEntity>().Count();
		}

		public async Task<int> CountAsync()
		{
			return await _context.Set<TEntity>().CountAsync();
		}

		public virtual void Save()
		{

			_context.SaveChanges();
		}

		public async virtual Task<int> SaveAsync()
		{
			return await _context.SaveChangesAsync();
		}

		public virtual IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
		{
			IQueryable<TEntity> query = _context.Set<TEntity>().Where(predicate);
			return query;
		}

		public virtual async Task<ICollection<TEntity>> FindByAsyn(Expression<Func<TEntity, bool>> predicate)
		{
			return await _context.Set<TEntity>().Where(predicate).ToListAsync();
		}

		private bool disposed = false;
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					_context.Dispose();
				}
				this.disposed = true;
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
