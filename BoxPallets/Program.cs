using BoxPallets.Model;


namespace BoxPallets;

public static class Program
{

    public static async Task Main(string[] args)
    {
        await using ApplicationContext db = new ApplicationContext();

        var boxes = db.Boxes.ToList();
        var pallets = db.Pallets.ToList();

        while (true)
        {
            Console.WriteLine("Введите \"1\": Сгруппировать все паллеты по сроку годности, отсортировать по возрастанию срока годности, в каждой группе отсортировать паллеты по весу");
            Console.WriteLine("Введите \"2\": 3 паллеты, которые содержат коробки с наибольшим сроком годности, отсортированные по возрастанию объема");
            Console.WriteLine("Введите \"3\": Добавить коробку ");
            Console.WriteLine("Введите \"4\": Добавить паллету");
            Console.WriteLine("Введите \"5\": Выход С сохранением изменений в БД");
            Console.WriteLine("Введите \"6\": Выход БЕЗ сохранения изменений");

            var desicionInput = Console.ReadLine();
            try
            {
                switch (desicionInput)
                {
                    case "1":
                        Hlp.PrintGroupedAndSorted(pallets.GroupsByExpiration());
                        break;
                    case "2":
                        Hlp.PrintTopVolume(pallets.TopVolume(3));
                        break;
                    case "3":
                        Console.WriteLine("Введите, параметры коробки:");
                        var boxId = Hlp.AskAndGetUserInputTo<int>("id: ");
                        var boxWidth = Hlp.AskAndGetUserInputTo<double>("width: ");
                        var boxHeight = Hlp.AskAndGetUserInputTo<double>("height: ");
                        var boxDepth = Hlp.AskAndGetUserInputTo<double>("depth: ");
                        var boxWeight = Hlp.AskAndGetUserInputTo<double>("weight: ");
                        var boxExpiration = DateOnly.FromDateTime(Hlp.AskAndGetUserInputTo<DateTime>("expiration: "));
                        var boxPalletId = Hlp.AskAndGetUserInputTo<int>("palletId: ");

                        if (!pallets.Any(x => x.Id == boxPalletId))
                        {
                            throw new ArgumentException("Pallet with specified ID do not exist");
                        }

                        var newBox = Box.WithExpiration(boxId, boxWidth, boxHeight, boxDepth, boxWeight, boxExpiration);
                        boxes.Add(newBox);
                        db.Boxes.Add(newBox);
                        var targetPallet = pallets.First(x => x.Id == boxPalletId);
                        targetPallet.AddBox(newBox);
                        db.Pallets.Update(targetPallet);
                        
                        break;
                    case "4":
                        var palletId = Hlp.AskAndGetUserInputTo<int>("id: ");
                        var palletWidth = Hlp.AskAndGetUserInputTo<double>("width: ");
                        var palletHeight = Hlp.AskAndGetUserInputTo<double>("height: ");
                        var palletDepth = Hlp.AskAndGetUserInputTo<double>("depth: ");
                        Pallet newPallet = new(palletId, palletWidth, palletHeight, palletDepth);
                        pallets.Add(newPallet);
                        db.Pallets.Add(newPallet);
                        await db.SaveChangesAsync();
                        break;
                    case "5":
                        await db.SaveChangesAsync();
                        return;
                    case "6":
                        return;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }

    
}