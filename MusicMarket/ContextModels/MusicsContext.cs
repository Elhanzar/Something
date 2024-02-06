using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MusicMarket.Models;

public partial class MusicsContext : DbContext
{
    public MusicsContext()
    {
    }

    public MusicsContext(DbContextOptions<MusicsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }

    public virtual DbSet<Track> Tracks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Сompany> Сompanies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;Port=5432;User Id=postgres;Password=Obkzavr3272;Database=Musics;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShoppingCart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("shopping_cart_pkey");

            entity.ToTable("Shopping_cart");

            entity.HasIndex(e => e.TrackId, "Tracks_unique").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"shopping_cart_Id_seq\"'::regclass)");
            entity.Property(e => e.Date).HasColumnType("time with time zone");
            entity.Property(e => e.TrackId).HasColumnName("Track_id");
            entity.Property(e => e.UserId).HasColumnName("User_id");

            entity.HasOne(d => d.Track).WithOne(p => p.ShoppingCart)
                .HasForeignKey<ShoppingCart>(d => d.TrackId)
                .HasConstraintName("Track_foreign");

            entity.HasOne(d => d.User).WithMany(p => p.ShoppingCarts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("User_foreign");
        });

        modelBuilder.Entity<Track>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Tracks_pkey");

            entity.HasIndex(e => e.MusicName, "Tracks_Music_name_key").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"Tracks_id_seq\"'::regclass)");
            entity.Property(e => e.AuthorId).HasColumnName("Author_id");
            entity.Property(e => e.AuthorName).HasColumnName("Author_name");
            entity.Property(e => e.CostPrice)
                .HasColumnType("money")
                .HasColumnName("Cost_price");
            entity.Property(e => e.DateRelize)
                .HasColumnType("time with time zone")
                .HasColumnName("Date_relize");
            entity.Property(e => e.Genre).HasDefaultValueSql("'noname'::text");
            entity.Property(e => e.GroupName)
                .HasDefaultValueSql("'noname'::text")
                .HasColumnName("Group_name");
            entity.Property(e => e.MusicName).HasColumnName("Music_name");
            entity.Property(e => e.Price).HasColumnType("money");

            entity.HasOne(d => d.Author).WithMany(p => p.Tracks)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("Tracks_Author_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.HasIndex(e => e.Email, "unique_email").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"users_Id_seq\"'::regclass)");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Patronymic)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .IsFixedLength();
        });

        modelBuilder.Entity<Сompany>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Сompany_pkey");

            entity.ToTable("Сompany");

            entity.HasIndex(e => new { e.Name, e.Email, e.PhoneNumber }, "Сompany_Name_Email_Phone_number_key").IsUnique();

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.PhoneNumber).HasColumnName("Phone_number");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
