using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Products.Entities;
using Restaurant.Domain.Products.Repositories;
using Serilog;

namespace Restaurant.Infrastructure.Persistent.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly RestaurantDbContext _dbContext;
    private readonly ILogger _logger;

    public CategoryRepository(RestaurantDbContext dbContext, ILogger logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<bool> Create(Category category)
    {
        FormattableString command =
            $"INSERT INTO public.\"Categories\" (\"CategoryId\", \"Alias\", \"Name\", \"Description\", \"ParentId\") VALUES ({category.CategoryId}, {category.Alias}, {category.Name}, {category.Description}, {category.ParentId})";
        try
        {
            var rawsCount = await _dbContext.Database.ExecuteSqlAsync(command);

            return rawsCount > 0;
        }
        catch(Exception ex)
        {
            _logger.Error(ex, $"Error with '{command}' sql command in CategoryRepository.");
            return false;
        }
    }

    public async Task<bool> Update(Category category)
    {
        FormattableString command =
            $"UPDATE public.\"Categories\" SET \"CategoryId\" = {category.CategoryId}, \"Alias\" = {category.Alias}, \"Name\" = {category.Name}, \"Description\" = {category.Description}, \"ParentId\" = {category.ParentId} WHERE \"CategoryId\" = {category.CategoryId}";
        try
        {
            var rawsCount = await _dbContext.Database.ExecuteSqlAsync(command);

            return rawsCount > 0;
        }
        catch(Exception ex)
        {
            _logger.Error(ex, $"Error with '{command}' sql command in CategoryRepository.");
            return false;
        }
    }

    public async Task<IEnumerable<Category>> Get()
    {
        FormattableString query = $"SELECT * FROM public.\"Categories\"";
        try
        {
            var categories = await _dbContext.Categories
                .FromSql(query)
                .ToListAsync();

            return categories;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error with '{query}' sql query in CategoryRepository.");
            throw;
        }
    }

    public async Task<Category?> GetByAlias(string alias)
    {
        FormattableString query = $"SELECT * FROM public.\"Categories\" WHERE \"Alias\" = {alias}";
        try
        {
            var product = await _dbContext.Categories
                .FromSql(query)
                .SingleOrDefaultAsync();

            return product;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error with '{query}' sql query in CategoryRepository.");
            throw;
        }
    }

    public async Task<bool> Delete(string alias)
    {
        FormattableString command = $"DELETE FROM public.\"Categories\" WHERE \"Alias\" = {alias}";
        try
        {
            var rawsCount = await _dbContext.Database.ExecuteSqlAsync(command);

            return rawsCount > 0;
        }
        catch(Exception ex)
        {
            _logger.Error(ex, $"Error with '{command}' sql command in CategoryRepository.");
            return false;
        }
    }

    public async Task<bool> IsAliasExist(string alias)
    {
        FormattableString query = $"SELECT * FROM public.\"Categories\" WHERE \"Alias\" = {alias}";
        try
        {
            var category = await _dbContext.Categories
                .FromSql(query)
                .SingleOrDefaultAsync();

            return category is not null;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error with '{query}' sql query in CategoryRepository.");
            throw;
        }
    }
}