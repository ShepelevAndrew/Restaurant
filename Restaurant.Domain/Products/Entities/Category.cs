using Restaurant.Domain.Common.Converters;

namespace Restaurant.Domain.Products.Entities;

public class Category
{
    private readonly List<Category> _subCategories = new();

    public Category(string name, string description, Guid? parentId)
    {
        CategoryId = Guid.NewGuid();
        Alias = AliasConverter.Convert(name, new Transliterator());
        Name = name;
        Description = description;
        ParentId = parentId;
    }

    public Guid CategoryId { get; private set; }

    public string Alias { get; private set; }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public Guid? ParentId { get; private set; }

    public IReadOnlyList<Category> SubCategories => _subCategories.AsReadOnly();

    public Category Update(string name, string description)
    {
        Name = name;
        Description = description;

        return this;
    }

    public void AddSubCategory(Category category)
    {
        _subCategories.Add(category);
    }
}