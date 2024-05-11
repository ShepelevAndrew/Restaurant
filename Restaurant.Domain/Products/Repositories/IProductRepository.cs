namespace Restaurant.Domain.Products.Repositories;

public interface IProductRepository
{
    Task<bool> Create(Product product);

    Task<bool> Update(Product product);

    Task<IEnumerable<Product>> Get();

    Task<Product?> GetById(Guid id);

    Task<Product?> GetByAlias(string alias);

    Task<IEnumerable<Product>> GetByCategory(Guid categoryId);

    Task<bool> Delete(string alias);

    Task<bool> IsAliasExist(string alias);
}