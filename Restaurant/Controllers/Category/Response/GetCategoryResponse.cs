namespace Restaurant.Controllers.Category.Response;

public record GetCategoryResponse(
    Guid CategoryId,
    string Alias,
    string Name,
    string Description,
    Guid? ParentId,
    List<CategoryResponse> SubCategories);