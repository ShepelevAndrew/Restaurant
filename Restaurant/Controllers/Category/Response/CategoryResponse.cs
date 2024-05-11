namespace Restaurant.Controllers.Category.Response;

public record CategoryResponse(
    Guid CategoryId,
    string Alias,
    string Name,
    string Description,
    Guid? ParentId);