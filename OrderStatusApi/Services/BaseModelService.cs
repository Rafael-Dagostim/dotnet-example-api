using Microsoft.EntityFrameworkCore;
using OrderStatusApi.Interfaces;
using OrderStatusApi.Models;

namespace OrderStatusApi.Services;

public abstract class BaseModelService<T> : IBaseModelService<T> where T : class
{
    protected readonly OrderDbContext _context;

    public BaseModelService(OrderDbContext context)
    {
        _context = context;
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _context
            .Set<T>()
            .ToListAsync();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _context
            .Set<T>()
            .FindAsync(id);
    }

    public async Task<T> CreateAsync(T newEntity)
    {
        await _context
            .Set<T>()
            .AddAsync(newEntity);

        await _context.SaveChangesAsync();

        return newEntity;
    }

    public async Task<T> AlterAsync(T newEntity)
    {
        _context
            .Set<T>()
            .Update(newEntity);

        await _context.SaveChangesAsync();

        return newEntity;
    }

    public async Task<T?> DeleteAsync(Guid id)
    {
        T? deletedEntity = await GetByIdAsync(id);
        if (deletedEntity is null) return null;

        _context.Remove(deletedEntity);
        await _context.SaveChangesAsync();

        return deletedEntity;
    }
}
