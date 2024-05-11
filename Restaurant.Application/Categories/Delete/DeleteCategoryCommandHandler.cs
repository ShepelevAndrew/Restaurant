using ErrorOr;
using MediatR;
using Restaurant.Domain.Errors;
using Restaurant.Domain.Products.Repositories;

namespace Restaurant.Application.Categories.Delete;

public class DeleteCategoryCommandHandler
    : IRequestHandler<DeleteCategoryCommand, ErrorOr<bool>>
{
    private readonly ICategoryRepository _categoryRepository;

    public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<ErrorOr<bool>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        if (!await _categoryRepository.IsAliasExist(request.Alias))
        {
            return Errors.Category.CategoryNotFound;
        }

        var isSuccess = await _categoryRepository.Delete(request.Alias);
        if (!isSuccess)
        {
            return Errors.Database.DatabaseFailure;
        }

        return true;
    }
}