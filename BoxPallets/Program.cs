using BoxPallets.Model;
using ConsoleTables;


namespace BoxPallets;

public static class Program
{
    public static void Main(string[] args)
    {
        using (ApplicationContext db = new ApplicationContext())
        {


            var boxes = db.Boxes.ToList();
            var pallets = db.Pallets.ToList();

            while (true)
            {
                Console.WriteLine("Введите \"1\": Сгруппировать все паллеты по сроку годности, отсортировать по возрастанию срока годности, в каждой группе отсортировать паллеты по весу");
                Console.WriteLine("Введите \"2\": 3 паллеты, которые содержат коробки с наибольшим сроком годности, отсортированные по возрастанию объема");
                Console.WriteLine("Введите \"3\": Выход");
                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        PrintGroupedAndSorted(pallets);
                        break;
                    case "2":
                        PrintTopVolume(pallets);
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }               
                
            }
        }

    }

    public static void PrintTopVolume(IEnumerable<Pallet> pallets)
    {
        var result = pallets.OrderByDescending(p => p.Boxes.Max(b => b.ExpirationDate))
                    .Take(3).OrderBy(p => p.Volume);
        var table = new ConsoleTable("Id", "Max box expiration", "Volume");
        foreach (var p in result)
        {
            table.AddRow(p.Id, p.Boxes.Max(b => b.ExpirationDate), p.Volume);
        }
        table.Write();
    }

    public static void PrintGroupedAndSorted(IEnumerable<Pallet> pallets)
    {
        var groupsByExpiration = pallets.GroupBy(p => p.ExpirationDate).OrderBy(x => x.Key);

        foreach (var group in groupsByExpiration)
        {
            Console.WriteLine($"Group {group.Key}");

            var table = new ConsoleTable("id", "Expiration", "Weight", "Volume", "Width", "Height", "Depth", "Boxes count");

            foreach (var p in group.OrderBy(p => p.Weight))
            {
                table.AddRow(p.Id, p.ExpirationDate, p.Weight, p.Volume, p.Width, p.Height, p.Depth, p.Boxes.Count);
            }
            table.Write();

            Console.WriteLine("--------------------------------------------------------------------\n\n");
        }
    }
}



/*    {

        Console.WriteLine("Список коробок:");

        foreach (var b in boxes)
        {
            Console.WriteLine($"id: {b.Id}, volume: {b.Volume}, expires at: {b.ExpirationDate}, weight: {b.Weight}");
        }
        Console.WriteLine("Список паллетов:");
        foreach (var p in pallets)
        {
            Console.WriteLine($"id: {p.Id}, volume: {p.Volume}, expires at: {p.ExpirationDate}, weight: {p.Weight}");
        }
    }*/

/*    var table = new ConsoleTable("id", "Expiration", "Weight", "Volume");
    table.Write();
    Console.WriteLine();


    foreach (var p in groupByExpiration)
    {

        table.AddRow(p.Id, p.ExpirationDate, p.Weight, p.Volume);
        table.Write();
    }
*//*    ConsoleTable
        .From<Pallet>(pallets.Select(x => ()))
        .Configure(o => o.NumberAlignment = Alignment.Right)
        .Write(Format.Alternative);*//*

    Console.ReadKey();*/



/*    List<int> possible = Enumerable.Range(1, 99999).ToList();
    Random rnd = new();
    List<Pallet> palletsToWrite = new();
    for (int i = 1; i < 101; i++)
    {
        double palletWidth = rnd.NextDouble() * 100;
        double palletHeight = rnd.NextDouble() * 100;
        double palletDepth = rnd.NextDouble() * 100;

        List<Box> boxesToWrite = new List<Box>();
        for (int j = 0; j < rnd.Next(10, 900); j++)
        {
            int index = rnd.Next(0, possible.Count);
            if (rnd.NextDouble() > 0.5)
                boxesToWrite.Add(Box.WithCreation(possible[index],
                        rnd.NextDouble() * palletWidth, rnd.NextDouble() * palletHeight,
                        rnd.NextDouble() * palletDepth, rnd.NextDouble() * 1000,
                        new DateOnly(rnd.Next(1, 10000), rnd.Next(1, 13), rnd.Next(1, 29))));
            else
                boxesToWrite.Add(Box.WithExpiration(possible[index],
                        rnd.NextDouble() * palletWidth, rnd.NextDouble() * palletHeight,
                        rnd.NextDouble() * palletDepth, rnd.NextDouble() * 1000,
                        new DateOnly(rnd.Next(1, 10000), rnd.Next(1, 13), rnd.Next(1, 29))));

            possible.RemoveAt(index);
        }
        await db.Boxes.AddRangeAsync(boxesToWrite);
        await db.SaveChangesAsync();
        palletsToWrite.Add(Pallet.WithBoxes(i, palletWidth, palletHeight, palletDepth, boxesToWrite));
    }
    await db.Pallets.AddRangeAsync(palletsToWrite);
    await db.SaveChangesAsync();*/
