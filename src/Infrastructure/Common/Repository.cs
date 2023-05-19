using System.Linq.Expressions;
using EShop.Domain.Common;
using EShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace EShop.Infrastructure.Common;

public class BaseRepository<T> : IRepository<T> where T : BaseAuditableEntity

{

    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _entity;
    string errorMessage = string.Empty;

    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;

        _entity = context.Set<T>();
    }

    public virtual IQueryable<T> GetAll()
    {
        return _entity;
    }

    public virtual T GetLast()
    {
        return _entity.OrderByDescending(r => r.Id).First();
    }

    public T Get(long id)
    {
        return _entity.SingleOrDefault(s => s.Id == id);
    }

    public void Insert(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        //entity.Created = DateTime.Now;
        //entity.CreatedBy = currentUserId;
        
        _entity.Add(entity);
    }

    public long InsertAndGetId(T entity)
    {

        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        //entity.Created = DateTime.Now;
        //entity.CreatedBy = currentUserId;
        _entity.Add(entity);
        SaveChanges();
        return entity.Id;

    }

    public void Update(T entity)
    {

        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        //entity.LastModified = DateTime.Now;
        //entity.LastModifiedBy = currentUserId;
    }

    public void Delete(int id)
    {
        var entity = Get(id);
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        //entity.Deleted = DateTime.Now;
        //entity.DeletedBy = currentUserId;
        _entity.Remove(entity);

    }

    public virtual IQueryable<T> GetBy(Expression<Func<T, bool>> predicate)
    {
        return _entity.Where(predicate);
    }

    public IQueryable<T> Get()
    {
        return _entity.AsQueryable();
    }

    public Task<int> SaveChanges()
    {
        return _context.SaveChangesAsync();
    }

}



