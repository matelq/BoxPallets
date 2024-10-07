using System.ComponentModel;
using BoxPallets.Model;
using ConsoleTables;

namespace BoxPallets;

public static class Hlp
{
    public static T AskAndGetUserInputTo<T>(string messageForUser)
    {
        Console.Write(messageForUser);
        var value = ConvertTo<T>(Console.ReadLine());
        if (value == null)
            throw new ArgumentNullException();
        return value;
    }

    private static T ConvertTo<T>(this object value)
    {
        T returnValue;

        if (value is T variable)
            returnValue = variable;
        else
            try
            {
                if (Nullable.GetUnderlyingType(typeof(T)) != null)
                {
                    TypeConverter conv = TypeDescriptor.GetConverter(typeof(T));
                    returnValue = (T)conv.ConvertFrom(value);
                }
                else
                {
                    returnValue = (T)Convert.ChangeType(value, typeof(T));
                }
            }
            catch (Exception)
            {
                returnValue = default(T);
            }

        return returnValue;
    }

    public static void PrintTopVolume(IEnumerable<Pallet> pallets)
    {
        var table = new ConsoleTable("Id", "Max box expiration", "Volume");
        foreach (var p in pallets)
        {
            table.AddRow(p.Id, p.Boxes.Max(b => b.ExpirationDate), p.Volume);
        }

        table.Write();
    }

    public static void PrintGroupedAndSorted(IOrderedEnumerable<IGrouping<DateOnly, Pallet>> groups)
    {
        foreach (var group in groups)
        {
            Console.WriteLine($"Group {group.Key}");

            var table = new ConsoleTable("id", "Expiration", "Weight", "Volume", "Width", "Height", "Depth",
                "Boxes count");

            foreach (var p in group.OrderBy(p => p.Weight))
            {
                table.AddRow(p.Id, p.ExpirationDate, p.Weight, p.Volume, p.Width, p.Height, p.Depth, p.Boxes.Count);
            }

            table.Write();

            Console.WriteLine("--------------------------------------------------------------------\n\n");
        }
    }
}