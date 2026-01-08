using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SSDKTB4.WebApi.Database.AppDbContextModels;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Festival> Festivals { get; set; }

    public virtual DbSet<FestivalManagement> FestivalManagements { get; set; }

    public virtual DbSet<Tbl3Product> Tbl3Products { get; set; }

    public virtual DbSet<TblProduct> TblProducts { get; set; }

 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Festival>(entity =>
        {
            entity.HasKey(e => e.FestivalId).HasName("PK__Festival__875D72CDA5E73CC5");

            entity.ToTable("Festival");

            entity.Property(e => e.CreatedDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FestivalDateTime).HasColumnType("datetime");
            entity.Property(e => e.FestivalName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Location).HasColumnType("text");
            entity.Property(e => e.ModifiedDateTime).HasColumnType("datetime");
            entity.Property(e => e.TicketPrice).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<FestivalManagement>(entity =>
        {
            entity.HasKey(e => e.FestivalId);

            entity.ToTable("festival_management");

            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.FestivalDateTime).HasColumnType("datetime");
            entity.Property(e => e.FestivalName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Location).HasColumnType("text");
            entity.Property(e => e.ModifiedDateTime).HasColumnType("datetime");
            entity.Property(e => e.TicketPrice).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<Tbl3Product>(entity =>
		{
            entity.HasKey(e => e.ProductId);
			entity.ToTable("Tbl3_Product");
			entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDateTime).HasColumnType("datetime");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.ProductId).ValueGeneratedOnAdd();
            entity.Property(e => e.ProductName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblProduct>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbl_product");
            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.ProductName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
