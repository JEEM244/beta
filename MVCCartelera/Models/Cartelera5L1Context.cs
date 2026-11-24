using Microsoft.EntityFrameworkCore;

namespace MVCCartelera.Models;

public class Cartelera5L1Context : DbContext
{
    public Cartelera5L1Context(DbContextOptions<Cartelera5L1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Movies5L1> Movies5L1 { get; set; }
    public virtual DbSet<Salas5L1> Salas5L1 { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movies5L1>()
            .HasOne(m => m.Sala)
            .WithMany(s => s.Movies5L1)
            .HasForeignKey(m => m.IdSala)
            .OnDelete(DeleteBehavior.Restrict);
    }
}