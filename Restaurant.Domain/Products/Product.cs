using Restaurant.Domain.Common.Converters;
using Restaurant.Domain.Products.ValueObjects;

namespace Restaurant.Domain.Products;

public class Product
{
    public Product(string name, decimal price, uint weight, string description, Guid categoryId)
    {
        ProductId = Guid.NewGuid();
        Alias = AliasConverter.Convert(name, new Transliterator());
        Name = name;
        Price = price;
        Weight = weight;
        Description = description;
        CategoryId = categoryId;
        AverageRating = AverageRating.Default;
    }

    // FOR EF CORE MAPPING
    private Product()
    {
    }

    public Guid ProductId { get; private set; }

    public string Alias { get; private set; } = string.Empty;

    public string Name { get; private set; } = string.Empty;

    public decimal Price { get; private set; }

    public uint Weight { get; private set; }

    public string Description { get; private set; } = string.Empty;

    public Guid CategoryId { get; private set; }

    public AverageRating AverageRating { get; private set; } = null!;

    public Product Update(string name, decimal price, uint weight, string description, Guid categoryId)
    {
        Name = name;
        Price = price;
        Weight = weight;
        Description = description;
        CategoryId = categoryId;

        return this;
    }

    public Product AddRating(int mark)
    {
        var rating = new Rating(mark);
        AverageRating.AddNewRating(rating);

        return this;
    }
}