using ErrorOr;
using MediatR;
using Restaurant.Domain.Errors;
using Restaurant.Domain.Products.Entities;
using Restaurant.Domain.Products.Repositories;

namespace Restaurant.Application.Categories.Update;

public class UpdateCategoryCommandHandler
    : IRequestHandler<UpdateCategoryCommand, ErrorOr<Category>>
{
    private readonly ICategoryRepository _categoryRepository;

    public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<ErrorOr<Category>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByAlias(request.Alias);
        if (category is null)
        {
            return Errors.Category.CategoryNotFound;
        }

        var oldAlias = category.Alias;
        var updatedCategory = category.Update(request.Name, request.Description);

        var isSameAlias = oldAlias == updatedCategory.Alias;
        if (await _categoryRepository.IsAliasExist(updatedCategory.Alias) && !isSameAlias)
        {
            return Errors.Category.DuplicateAlias;
        }

        var isSuccess = await _categoryRepository.Update(updatedCategory);
        if (!isSuccess)
        {
            return Errors.Database.DatabaseFailure;
        }

        return updatedCategory;
    }
}