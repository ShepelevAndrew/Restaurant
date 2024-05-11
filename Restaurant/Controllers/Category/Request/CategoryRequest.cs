namespace Restaurant.Controllers.Category.Request;

public record CategoryRequest(
    string Name,
    string Description,
    Guid? ParentId);