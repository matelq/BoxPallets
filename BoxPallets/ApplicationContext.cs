using BoxPallets.Model;
using Microsoft.EntityFrameworkCore;

namespace BoxPallets;

public class ApplicationContext : DbContext
{
    public DbSet<Box> Boxes => Set<Box>();
    public DbSet<Pallet> Pallets => Set<Pallet>();
    public ApplicationContext()
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source= ../../../BoxPallets.db");
    }
}
