namespace Restaurant.Domain.Products.ValueObjects;

public class Rating
{
    public Rating(int value)
    {
        Value = value;
    }

    public int Value { get; private set; }
}