using Maplr.SuggarShack.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Maplr.SuggarShack.Data.Context;

public class MaplrContext : DbContext
{
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<OrderItems> OrderItems { get; set; }

    public MaplrContext(DbContextOptions<MaplrContext> options)
 : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<OrderItems>(entity =>
        {
            entity.HasOne(d => d.Order)
                .WithMany(d => d.Items)
                .HasForeignKey(d => d.OrderId);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.Name)
                .IsRequired(true)
                .HasMaxLength(200);

            entity.Property(e => e.Description)
                .IsRequired(true);

            AddDefaultData(entity);
        });
    }

    private void AddDefaultData(EntityTypeBuilder<Product> entity)
    {
        entity.HasData(new Product()
        {
            Id = Guid.NewGuid(),
            Description = "Fine nose that opens to the scents of maple and a quite supported final. In mouth, it has a pure taste and an ample texture, unveiling fully the maple aromas.",
            Name = "AMBER MAPLE SYRUP",
            Type = Domain.Enum.ProductType.AMBER,
            Price = 12.95,
            Stock = 100,
            Image = "https://www.camaplepremium.com/wp-content/uploads/2017/11/Sirop_amber_250ml-800x533.jpg"
        },
        new Product()
        {
            Id = Guid.NewGuid(),
            Description = "Strong nose that opens on a caramel and butter perfume. In mouth, a franc maple flavor and a creamy texture.",
            Name = "DARK MAPLE SYRUP",
            Type = Domain.Enum.ProductType.DARK,
            Price = 7.95,
            Stock = 100,
            Image = "https://www.camaplepremium.com/wp-content/uploads/2018/05/Sirop_dark_100ml.jpg"
        },
        new Product()
        {
            Id = Guid.NewGuid(),
            Description = "Soft nose that opens on a caramel and butter perfume. In mouth, a franc maple flavor and a creamy texture.",
            Name = "CLEAR MAPLE SYRUP",
            Type = Domain.Enum.ProductType.CLEAR,
            Price = 6.95,
            Stock = 100,
            Image = "https://www.finemapleproducts.com/wp-content/uploads/2018/06/803_2461.jpg"
        });
    }
}