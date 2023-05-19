using System.Linq.Expressions;

namespace EShop.Domain.Common;

public interface IRepository<T> where T : BaseEntity
{

    IQueryable<T> GetAll();

    T Get(long id);
    T GetLast();
    void Insert(T entity);
    long InsertAndGetId(T entity);
    void Update(T entity);
    void Delete(int id);
    IQueryable<T> GetBy(Expression<Func<T, bool>> predicate);
    IQueryable<T> Get();
    Task<int> SaveChanges();



}



