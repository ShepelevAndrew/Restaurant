using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Products;
using Restaurant.Domain.Products.Repositories;
using Serilog;

namespace Restaurant.Infrastructure.Persistent.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly RestaurantDbContext _dbContext;
    private readonly ILogger _logger;

    public ProductRepository(RestaurantDbContext dbContext, ILogger logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<bool> Create(Product product)
    {
        FormattableString command =
            $"INSERT INTO public.\"Products\" (\"ProductId\", \"Alias\", \"Name\", \"Price\", \"Weight\", \"Description\", \"CategoryId\", \"AverageRating_Value\", \"AverageRating_NumRatings\") VALUES ({product.ProductId}, {product.Alias}, {product.Name}, {product.Price}, {product.Weight}, {product.Description}, {product.CategoryId}, {product.AverageRating.Value}, {product.AverageRating.NumRatings})";
        try
        {
            var rawsCount = await _dbContext.Database.ExecuteSqlAsync(command);

            return rawsCount > 0;
        }
        catch(Exception ex)
        {
            _logger.Error(ex, $"Error with '{command}' sql command in ProductRepository.");
            return false;
        }
    }

    public async Task<bool> Update(Product product)
    {
        FormattableString command =
            $"UPDATE public.\"Products\" SET \"ProductId\" = {product.ProductId}, \"Alias\" = {product.Alias}, \"Name\" = {product.Name}, \"Price\" = {product.Price}, \"Weight\" = {product.Weight}, \"Description\" = {product.Description}, \"CategoryId\" = {product.CategoryId}, \"AverageRating_Value\" = {product.AverageRating.Value}, \"AverageRating_NumRatings\" = {product.AverageRating.NumRatings} WHERE \"ProductId\" = {product.ProductId}";
        try
        {
            var rawsCount = await _dbContext.Database.ExecuteSqlAsync(command);

            return rawsCount > 0;
        }
        catch(Exception ex)
        {
            _logger.Error(ex, $"Error with '{command}' sql command in ProductRepository.");
            return false;
        }
    }

    public async Task<IEnumerable<Product>> Get()
    {
        FormattableString query = $"SELECT * FROM public.\"Products\"";
        try
        {
            var products = await _dbContext.Products
                .FromSql(query)
                .ToListAsync();

            return products;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error with '{query}' sql query in ProductRepository.");
            throw;
        }
    }

    public async Task<Product?> GetById(Guid id)
    {
        FormattableString query = $"SELECT * FROM public.\"Products\" WHERE \"ProductId\" = {id}";
        try
        {
            var product = await _dbContext.Products
                .FromSql(query)
                .SingleOrDefaultAsync();

            return product;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error with '{query}' sql query in ProductRepository.");
            throw;
        }
    }

    public async Task<Product?> GetByAlias(string alias)
    {
        FormattableString query = $"SELECT * FROM public.\"Products\" WHERE \"Alias\" = {alias}";
        try
        {
            var product = await _dbContext.Products
                .FromSql(query)
                .SingleOrDefaultAsync();

            return product;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error with '{query}' sql query in ProductRepository.");
            throw;
        }
    }

    public async Task<IEnumerable<Product>> GetByCategory(Guid categoryId)
    {
        FormattableString query = $"SELECT * FROM public.\"Products\" WHERE \"CategoryId\" = {categoryId}";
        try
        {
            var products = await _dbContext.Products
                .FromSql(query)
                .ToListAsync();

            return products;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error with '{query}' sql query in ProductRepository.");
            throw;
        }
    }

    public async Task<bool> Delete(string alias)
    {
        FormattableString command = $"DELETE FROM public.\"Products\" WHERE \"Alias\" = {alias}";
        try
        {
            var rawsCount = await _dbContext.Database.ExecuteSqlAsync(command);

            return rawsCount > 0;
        }
        catch(Exception ex)
        {
            _logger.Error(ex, $"Error with '{command}' sql command in ProductRepository.");
            return false;
        }
    }

    public async Task<bool> IsAliasExist(string alias)
    {
        FormattableString query = $"SELECT * FROM public.\"Products\" WHERE \"Alias\" = {alias}";
        try
        {
            var product = await _dbContext.Products
                .FromSql(query)
                .SingleOrDefaultAsync();

            return product is not null;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error with '{query}' sql query in ProductRepository.");
            throw;
        }
    }
}