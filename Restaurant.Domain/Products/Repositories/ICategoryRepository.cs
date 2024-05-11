using Restaurant.Domain.Products.Entities;

namespace Restaurant.Domain.Products.Repositories;

public interface ICategoryRepository
{
    Task<bool> Create(Category category);

    Task<bool> Update(Category category);

    Task<IEnumerable<Category>> Get();

    Task<Category?> GetByAlias(string alias);

    Task<bool> Delete(string alias);

    Task<bool> IsAliasExist(string alias);
}