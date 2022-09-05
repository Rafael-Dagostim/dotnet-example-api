namespace OrderStatusApi.Interfaces;

public interface IBaseModelService<T> where T : class
{
    public Task<List<T>> GetAllAsync();
    
    public Task<T?> GetByIdAsync(Guid id);
    
    public Task<T> CreateAsync(T newEntity);
    
    public Task<T> AlterAsync(T newEntity);
    
    public Task<T?> DeleteAsync(Guid id);
}