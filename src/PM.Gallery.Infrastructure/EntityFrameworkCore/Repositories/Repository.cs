using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PM.Gallery.Domain.IRepository;
using PM.Gallery.Domain.ISpecification;
using PM.Gallery.Infrastructure.EntityFrameworkCore.Specifications;

namespace PM.Gallery.Infrastructure.EntityFrameworkCore.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<Repository<TEntity>> _logger;

    public Repository(AppDbContext dbContext, ILogger<Repository<TEntity>> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public ISpecificationTools<TEntity> UseSpecification()
    {
        return new SpecificationTools<TEntity>(_dbContext.Set<TEntity>().AsQueryable());
    }

    public async Task AddAsync(TEntity entity)
    {
        await _dbContext.AddAsync(entity);
        if (await _dbContext.SaveChangesAsync() == 0)
        {
            _logger.LogError(
                $"Failed to add entity {entity.GetType().Name} with id {entity.GetType().GetProperty("Id")!.GetValue(entity)}");
            throw new DbUpdateException();
        }
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _dbContext.Update(entity);
        if (await _dbContext.SaveChangesAsync() == 0)
        {
            _logger.LogError(
                $"Failed to Update entity {entity.GetType().Name} with id {entity.GetType().GetProperty("Id")!.GetValue(entity)}");
            throw new DbUpdateException();
        }
    }

    public async Task DeleteAsync(TEntity entity)
    {
        _dbContext.Remove(entity);
        if (await _dbContext.SaveChangesAsync() == 0)
        {
            _logger.LogError(
                $"Failed to Remove entity {entity.GetType().Name} with id {entity.GetType().GetProperty("Id")!.GetValue(entity)}");
            throw new DbUpdateException();
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _dbContext.Set<TEntity>().FindAsync(id);
        if (entity is null) throw new KeyNotFoundException();
        _dbContext.Remove(entity);
        if (await _dbContext.SaveChangesAsync() == 0)
        {
            _logger.LogError(
                $"Failed to Remove entity {entity.GetType().Name} with id {entity.GetType().GetProperty("Id")!.GetValue(entity)}");
            throw new DbUpdateException();
        }
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entitys)
    {
        await _dbContext.AddRangeAsync(entitys);
        if (await _dbContext.SaveChangesAsync() == 0)
        {
            _logger.LogError($"Failed to Add entitys of type {entitys.GetType().Name}");
            throw new DbUpdateException();
        }
    }

    public async Task UpdateRangeAsync(IEnumerable<TEntity> entitys)
    {
        _dbContext.UpdateRange(entitys);
        if (await _dbContext.SaveChangesAsync() == 0)
        {
            _logger.LogError($"Failed to Update entitys of type {entitys.GetType().Name}");
            throw new DbUpdateException();
        }
    }

    public async Task DeleteRangeAsync(IEnumerable<TEntity> entitys)
    {
        _dbContext.RemoveRange(entitys);
        if (await _dbContext.SaveChangesAsync() == 0)
        {
            _logger.LogError($"Failed to Remove entitys of type {entitys.GetType().Name}");
            throw new DbUpdateException();
        }
    }

    public async Task DeleteRangeAsync(IEnumerable<Guid> ids)
    {
        foreach (var id in ids)
        {
            await DeleteAsync(id);
        }
    }

    public bool Contains(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbContext.Set<TEntity>().Where(predicate).Any();
    }

    public int Count(Expression<Func<TEntity, bool>>? predicate = null)
    {
        return predicate is null ? 
            _dbContext.Set<TEntity>().Count() 
            : _dbContext.Set<TEntity>().Where(predicate).Count();
    }
}