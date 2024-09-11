using System.Collections.Immutable;

namespace BoxPallets.Model;

public class Pallet : BaseStorageObject
{
    private const double PalletBaseWeight = 30;
    public Pallet(int id, double width, double height, double depth)
        : base(id, width, height, depth, PalletBaseWeight)
    {
    }

    public override double Weight => Boxes.Sum(b => b.Weight) + base.Weight;
    public override double Volume => Boxes.Sum(b => b.Volume) + Height * Width * Depth;
    public override DateOnly ExpirationDate => Boxes.Count != 0 ? Boxes.Min(b => b.ExpirationDate) : DateOnly.MinValue;

    private IEnumerable<Box> boxes = new List<Box>();
    public IReadOnlyCollection<Box> Boxes => boxes.ToImmutableList();


    public void SetBoxes(IEnumerable<Box> newBoxes)
    {
        ThrowIfAnyBoxNotFitting(newBoxes);
        boxes = newBoxes.ToList();
    }

    public void AddBoxes(IEnumerable<Box> boxesToAdd)
    {
        ThrowIfAnyBoxNotFitting(boxesToAdd);
        boxesToAdd = boxesToAdd.ToList();
    }

    public void ThrowIfAnyBoxNotFitting(IEnumerable<Box> boxes)
    {
        if (boxes.Count() != 0 && boxes.Where(b => !IsBoxFitting(b)).Any())
            throw new ArgumentOutOfRangeException();
    }

    public bool IsBoxFitting(Box box)
    {
        if (box.Depth > Depth || box.Width > Width)
            return false;
        return true;
    }

    public static Pallet WithBoxes(int id, double width, double height, double depth, IEnumerable<Box> boxes)
    {
        Pallet pallet = new(id, width, height, depth);
        pallet.SetBoxes(boxes);
        return pallet;
    }
}
