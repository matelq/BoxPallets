using BoxPallets.Model;

namespace BoxPallets.Tests
{
    public class BoxTests
    {
        [Fact]
        public void WithExpiration()
        {
            var b = Box.WithExpiration(1, 1, 1, 1, 1, new DateOnly(2012, 12, 12));
            Assert.Equal(1, b.Volume);
            Assert.Equal(new DateOnly(2012, 12, 12), b.ExpirationDate);
        }

        [Fact]
        public void WithCreation()
        {
            var b = Box.WithCreation(1, 12, 13, 14, 1, new DateOnly(2012, 12, 12));
            Assert.Equal(12 * 13 * 14, b.Volume);
            Assert.Equal(new DateOnly(2013, 3, 22), b.ExpirationDate);
        }


    }
}