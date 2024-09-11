namespace BoxPallets.Model;

public class Box : BaseStorageObject
{

    const int ExpirationFromCreationDays = 100;
    public DateOnly? CreationDate { get; init; }
    public DateOnly? InternalExpirationDate { get; init; }

    private Box(int id, double width, double height, double depth, double weight)
        : base(id, width, height, depth, weight)
    {
    }

    private Box(int id, double width, double height, double depth, double weight, DateOnly expirationDate, DateOnly creationDate)
    : base(id, width, height, depth, weight)
    {
        CreationDate = creationDate;
        InternalExpirationDate = expirationDate;
    }

    public override double Volume => Height * Width * Depth;

    public override DateOnly ExpirationDate
    {
        get
        {
            if (InternalExpirationDate.HasValue)
                return InternalExpirationDate.Value;
            if (CreationDate.HasValue)
                return CreationDate.Value.AddDays(100);
            throw new ArgumentNullException(nameof(ExpirationDate));
        }
    }


    public static Box WithExpiration(int id, double width, double height, double depth, double weight, DateOnly expirationDate)
    {
        return new Box(id, width, height, depth, weight)
        {
            InternalExpirationDate = expirationDate,
        };
    }

    public static Box WithCreation(int id, double width, double height, double depth, double weight, DateOnly creationDate)
    {
        return new Box(id, width, height, depth, weight)
        {
            CreationDate = creationDate,
        };
    }
}
