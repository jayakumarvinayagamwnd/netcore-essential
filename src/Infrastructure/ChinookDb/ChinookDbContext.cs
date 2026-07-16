using ChinookDb.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChinookDb;

public class ChinookDbContext : DbContext
{
    public ChinookDbContext(DbContextOptions<ChinookDbContext> options) : base(options)
    {
    }

    public DbSet<Artist> Artists => Set<Artist>();
    public DbSet<Album> Albums => Set<Album>();
    public DbSet<Track> Tracks => Set<Track>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<MediaType> MediaTypes => Set<MediaType>();
    public DbSet<Playlist> Playlists => Set<Playlist>();
    public DbSet<PlaylistTrack> PlaylistTracks => Set<PlaylistTrack>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<InvoiceLine> InvoiceLines => Set<InvoiceLine>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Artist
        modelBuilder.Entity<Artist>(entity =>
        {
            entity.ToTable("Artists");
            entity.HasKey(e => e.ArtistId);
            entity.Property(e => e.Name).HasMaxLength(120);
        });

        // Album
        modelBuilder.Entity<Album>(entity =>
        {
            entity.ToTable("Albums");
            entity.HasKey(e => e.AlbumId);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(160);
            entity.HasOne(e => e.Artist)
                  .WithMany(a => a.Albums)
                  .HasForeignKey(e => e.ArtistId);
        });

        // Track
        modelBuilder.Entity<Track>(entity =>
        {
            entity.ToTable("Tracks");
            entity.HasKey(e => e.TrackId);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Composer).HasMaxLength(220);
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(10,2)");
            entity.HasOne(e => e.Album)
                  .WithMany(a => a.Tracks)
                  .HasForeignKey(e => e.AlbumId);
            entity.HasOne(e => e.MediaType)
                  .WithMany(m => m.Tracks)
                  .HasForeignKey(e => e.MediaTypeId);
            entity.HasOne(e => e.Genre)
                  .WithMany(g => g.Tracks)
                  .HasForeignKey(e => e.GenreId);
        });

        // Genre
        modelBuilder.Entity<Genre>(entity =>
        {
            entity.ToTable("Genres");
            entity.HasKey(e => e.GenreId);
            entity.Property(e => e.Name).HasMaxLength(120);
        });

        // MediaType
        modelBuilder.Entity<MediaType>(entity =>
        {
            entity.ToTable("MediaTypes");
            entity.HasKey(e => e.MediaTypeId);
            entity.Property(e => e.Name).HasMaxLength(120);
        });

        // Playlist
        modelBuilder.Entity<Playlist>(entity =>
        {
            entity.ToTable("Playlists");
            entity.HasKey(e => e.PlaylistId);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(120);
        });

        // PlaylistTrack (composite key)
        modelBuilder.Entity<PlaylistTrack>(entity =>
        {
            entity.ToTable("PlaylistTracks");
            entity.HasKey(e => new { e.PlaylistId, e.TrackId });
            entity.HasOne(e => e.Playlist)
                  .WithMany(p => p.PlaylistTracks)
                  .HasForeignKey(e => e.PlaylistId);
            entity.HasOne(e => e.Track)
                  .WithMany(t => t.PlaylistTracks)
                  .HasForeignKey(e => e.TrackId);
        });

        // Customer
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customers");
            entity.HasKey(e => e.CustomerId);
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(40);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Company).HasMaxLength(80);
            entity.Property(e => e.Address).HasMaxLength(70);
            entity.Property(e => e.City).HasMaxLength(40);
            entity.Property(e => e.State).HasMaxLength(40);
            entity.Property(e => e.Country).HasMaxLength(40);
            entity.Property(e => e.PostalCode).HasMaxLength(10);
            entity.Property(e => e.Phone).HasMaxLength(24);
            entity.Property(e => e.Fax).HasMaxLength(24);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(60);
            entity.HasOne(e => e.SupportRep)
                  .WithMany(e => e.Customers)
                  .HasForeignKey(e => e.SupportRepId)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        // Employee
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employees");
            entity.HasKey(e => e.EmployeeId);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(20);
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Title).HasMaxLength(30);
            entity.Property(e => e.Address).HasMaxLength(70);
            entity.Property(e => e.City).HasMaxLength(40);
            entity.Property(e => e.State).HasMaxLength(40);
            entity.Property(e => e.Country).HasMaxLength(40);
            entity.Property(e => e.PostalCode).HasMaxLength(10);
            entity.Property(e => e.Phone).HasMaxLength(24);
            entity.Property(e => e.Fax).HasMaxLength(24);
            entity.Property(e => e.Email).HasMaxLength(60);
            entity.HasOne(e => e.Manager)
                  .WithMany(e => e.DirectReports)
                  .HasForeignKey(e => e.ReportsTo)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        // Invoice
        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.ToTable("Invoices");
            entity.HasKey(e => e.InvoiceId);
            entity.Property(e => e.BillingAddress).HasMaxLength(70);
            entity.Property(e => e.BillingCity).HasMaxLength(40);
            entity.Property(e => e.BillingState).HasMaxLength(40);
            entity.Property(e => e.BillingCountry).HasMaxLength(40);
            entity.Property(e => e.BillingPostalCode).HasMaxLength(10);
            entity.Property(e => e.Total).HasColumnType("decimal(10,2)");
            entity.HasOne(e => e.Customer)
                  .WithMany(c => c.Invoices)
                  .HasForeignKey(e => e.CustomerId);
        });

        // InvoiceLine
        modelBuilder.Entity<InvoiceLine>(entity =>
        {
            entity.ToTable("InvoiceLines");
            entity.HasKey(e => e.InvoiceLineId);
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(10,2)");
            entity.HasOne(e => e.Invoice)
                  .WithMany(i => i.InvoiceLines)
                  .HasForeignKey(e => e.InvoiceId);
            entity.HasOne(e => e.Track)
                  .WithMany(t => t.InvoiceLines)
                  .HasForeignKey(e => e.TrackId);
        });
    }
}