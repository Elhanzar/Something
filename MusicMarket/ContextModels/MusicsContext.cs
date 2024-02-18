using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MusicMarket.Models;

namespace MusicMarket.ContextModels;

public partial class MusicsContext : DbContext
{
    public MusicsContext()
    {
    }

    public MusicsContext(DbContextOptions<MusicsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Track> Tracks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public string GetConnectionString()
    {
        var builder = new ConfigurationBuilder();
        // установка пути к текущему каталогу
        builder.SetBasePath(Directory.GetCurrentDirectory());
        // получаем конфигурацию из файла appsettings.json
        builder.AddJsonFile("appsettings.json");
        // создаем конфигурацию
        var config = builder.Build();
        // получаем строку подключения
        string connectionString = config.GetConnectionString("DefaultConnection");

        return connectionString;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(GetConnectionString());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Company_pkey");

            entity.ToTable("company");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('\"Company_id_seq\"'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Number).HasColumnName("number");
            entity.Property(e => e.Password).HasColumnName("password");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("orders_pkey");

            entity.ToTable("orders");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Trackid).HasColumnName("trackid");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Track).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Trackid)
                .HasConstraintName("orders_trackid_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("orders_userid_fkey");
        });

        modelBuilder.Entity<Track>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("track_pkey");

            entity.ToTable("track");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Authorid).HasColumnName("authorid");
            entity.Property(e => e.Authorname).HasColumnName("authorname");
            entity.Property(e => e.Costprice).HasColumnName("costprice");
            entity.Property(e => e.Daterelize)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("daterelize");
            entity.Property(e => e.Genre)
                .HasDefaultValueSql("1")
                .HasColumnName("genre");
            entity.Property(e => e.Groupname).HasColumnName("groupname");
            entity.Property(e => e.Musicname).HasColumnName("musicname");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Author).WithMany(p => p.Tracks)
                .HasForeignKey(d => d.Authorid)
                .HasConstraintName("track_authorid_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_pkey");

            entity.ToTable("user");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Patronymic).HasColumnName("patronymic");
            entity.Property(e => e.Surname).HasColumnName("surname");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
