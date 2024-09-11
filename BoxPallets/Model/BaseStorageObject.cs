using System.ComponentModel.DataAnnotations;

namespace BoxPallets.Model;

public abstract class BaseStorageObject
{
    [Key]
    public int Id { get; private set; }
    public double Width { get; private set; }
    public double Height { get; private set; }
    public double Depth { get; private set; }
    public virtual double Weight { get; private set; }
    public abstract double Volume { get; }
    public abstract DateOnly ExpirationDate { get; }

    protected BaseStorageObject(int id, double width, double height, double depth, double weight)
    {
        Id = id;
        Width = width;
        Height = height;
        Depth = depth;
        Weight = weight;
    }
}
