namespace Week5_StudentApi.Data;

public class InMemoryRepository<T> : IRepository<T>
    where T : class
{
    private readonly List<T> records = new();

    public void Add(T entity)
    {
        records.Add(entity);
    }

    public List<T> GetAll()
    {
        return records;
    }

    public T? GetById(int id)
    {
        var idProperty = typeof(T).GetProperty("Id");

        return records.FirstOrDefault(entity =>
        {
            var value = idProperty?.GetValue(entity);

            return value is int entityId && entityId == id;
        });
    }

    public void Update(T entity)
    {
        var idProperty = typeof(T).GetProperty("Id");

        var entityId = idProperty?.GetValue(entity);

        if (entityId is not int id)
        {
            return;
        }

        var existingEntity = GetById(id);

        if (existingEntity == null)
        {
            return;
        }

        var properties = typeof(T).GetProperties();

        foreach (var property in properties)
        {
            if (property.CanWrite)
            {
                property.SetValue(
                    existingEntity,
                    property.GetValue(entity));
            }
        }
    }

    public void Delete(int id)
    {
        var existingEntity = GetById(id);

        if (existingEntity != null)
        {
            records.Remove(existingEntity);
        }
    }
}