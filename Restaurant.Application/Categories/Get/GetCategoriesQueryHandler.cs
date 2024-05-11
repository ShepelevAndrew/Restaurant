using ErrorOr;
using MediatR;
using Restaurant.Domain.Products.Entities;
using Restaurant.Domain.Products.Repositories;

namespace Restaurant.Application.Categories.Get;

public class GetCategoriesQueryHandler
    : IRequestHandler<GetCategoriesQuery, ErrorOr<List<Category>>>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoriesQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<ErrorOr<List<Category>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _categoryRepository.Get();
        return categories.ToList();
    }
}