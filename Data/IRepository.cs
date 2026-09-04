using System.Collections.Generic;

namespace Week5_StudentApi.Data;

public interface IRepository<T>
{
    void Add(T entity);
    List<T> GetAll();
    T? GetById(int id);
    void Update(T entity);
    void Delete(int id);
}