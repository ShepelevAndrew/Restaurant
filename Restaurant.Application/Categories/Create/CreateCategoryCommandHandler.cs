using ErrorOr;
using MediatR;
using Restaurant.Domain.Errors;
using Restaurant.Domain.Products.Entities;
using Restaurant.Domain.Products.Repositories;

namespace Restaurant.Application.Categories.Create;

public class CreateCategoryCommandHandler
    : IRequestHandler<CreateCategoryCommand, ErrorOr<Category>>
{
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<ErrorOr<Category>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category(request.Name, request.Description, request.ParentId);
        if (await _categoryRepository.IsAliasExist(category.Alias))
        {
            return Errors.Category.DuplicateAlias;
        }

        var isSuccess = await _categoryRepository.Create(category);
        if (!isSuccess)
        {
            return Errors.Database.DatabaseFailure;
        }

        return category;
    }
}