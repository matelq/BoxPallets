using BoxPallets.Model;

namespace BoxPallets;

public static class EnumerablePalletExtensions
{
    public static IEnumerable<Pallet> TopVolume(this IEnumerable<Pallet> pallets, int takeAmount)
    {
        return pallets
                .OrderByDescending(p => p.Boxes.Max(b => b.ExpirationDate))
                .Take(takeAmount)
                .OrderBy(p => p.Volume);
    }

    public static IOrderedEnumerable<IGrouping<DateOnly, Pallet>> GroupsByExpiration(this IEnumerable<Pallet> pallets)
    {
        return pallets
                .GroupBy(p => p.ExpirationDate)
                .OrderBy(x => x.Key);
    }
    
    
}