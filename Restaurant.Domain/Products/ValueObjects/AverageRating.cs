namespace Restaurant.Domain.Products.ValueObjects;

public class AverageRating
{
    private AverageRating(double value, uint numRatings)
    {
        Value = value;
        NumRatings = numRatings;
    }

    public static AverageRating Default => new(0, 0);

    public double Value { get; private set; }

    public uint NumRatings { get; private set; }

    public void AddNewRating(Rating rating)
    {
        Value = (Value * NumRatings + rating.Value) / ++NumRatings;
    }

    public void RemoveRating(Rating rating)
    {
        if(NumRatings == 0) return;

        Value = (Value * NumRatings - rating.Value) / --NumRatings;
    }
}