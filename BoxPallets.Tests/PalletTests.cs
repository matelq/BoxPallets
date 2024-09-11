using BoxPallets.Model;

namespace BoxPallets.Tests;

public class PalletTests
{
    List<Box> testBoxesList = new List<Box>();
    public PalletTests()
    {
        for (int i = 1; i < 11; i++)
        {
            testBoxesList.Add(Box.WithExpiration(i, i, i, i, i, new DateOnly(2024, 10, 9)));
        }
        for (int i = 11; i < 21; i++)
        {
            testBoxesList.Add(Box.WithCreation(i, i, i, i, i, new DateOnly(2024, 10, 9)));
        }
    }

    [Fact]
    public void EmptyPallet()
    {
        Pallet p = new(1, 12, 13, 14);
        Assert.Equal(30, p.Weight);
        Assert.Equal(DateOnly.MinValue, p.ExpirationDate);
        Assert.Equal(12 * 13 * 14, p.Volume);
    }

    [Fact]
    public void IncorrectBoxes()
    {
        Pallet p = new(1, 12, 13, 14);
        Assert.Throws<ArgumentOutOfRangeException>(() => p.AddBoxes(testBoxesList));
        Assert.Throws<ArgumentOutOfRangeException>(() => p.SetBoxes(testBoxesList));
        Assert.Throws<ArgumentOutOfRangeException>(() => Pallet.WithBoxes(1, 12, 13, 14, testBoxesList));
    }

    [Fact]
    public void CorrectBoxes()
    {
        Pallet p = Pallet.WithBoxes(1, 21, 5, 21, testBoxesList);

        Assert.Equal(240, p.Weight);
        Assert.Equal(46305, p.Volume);
        Assert.Equal(new DateOnly(2024, 10, 09), p.ExpirationDate);
    }
}
