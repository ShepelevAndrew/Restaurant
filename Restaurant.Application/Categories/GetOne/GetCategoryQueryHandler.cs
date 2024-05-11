using ErrorOr;
using MediatR;
using Restaurant.Domain.Errors;
using Restaurant.Domain.Products.Entities;
using Restaurant.Domain.Products.Repositories;

namespace Restaurant.Application.Categories.GetOne;

public class GetCategoryQueryHandler
    : IRequestHandler<GetCategoryQuery, ErrorOr<Category>>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoryQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<ErrorOr<Category>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByAlias(request.Alias);
        if (category is null)
        {
            return Errors.Category.CategoryNotFound;
        }

        return category;
    }
}