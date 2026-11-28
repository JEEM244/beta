using CineCarteleraV2_5L1.Models;
using Microsoft.EntityFrameworkCore;

namespace CineCarteleraV2_5L1.Data;

public class CineCarteleraContext : DbContext
{
    public CineCarteleraContext(DbContextOptions<CineCarteleraContext> options)
        : base(options)
    {
    }

    public DbSet<Pelicula> Peliculas => Set<Pelicula>();
    public DbSet<Sala> Salas => Set<Sala>();
    public DbSet<Funcion> Funciones => Set<Funcion>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pelicula>(entity =>
        {
            entity.ToTable("Peliculas", table =>
                table.HasCheckConstraint("CK_Peliculas_Duracion", "[DuracionMinutos] > 0"));

            entity.Property(p => p.Titulo).HasMaxLength(120).IsRequired();
            entity.Property(p => p.Genero).HasMaxLength(50).IsRequired();
            entity.Property(p => p.Clasificacion).HasMaxLength(10).IsRequired();
            entity.Property(p => p.FechaEstreno).HasColumnType("date");
            entity.Property(p => p.Sinopsis).HasMaxLength(600);
            entity.Property(p => p.Disponible).HasDefaultValue(true);
        });

        modelBuilder.Entity<Sala>(entity =>
        {
            entity.ToTable("Salas", table =>
                table.HasCheckConstraint("CK_Salas_Capacidad", "[Capacidad] > 0"));

            entity.Property(s => s.NombreSala).HasMaxLength(40).IsRequired();
            entity.Property(s => s.FormatoSala).HasMaxLength(30).IsRequired();
            entity.Property(s => s.Activa).HasDefaultValue(true);
        });

        modelBuilder.Entity<Funcion>(entity =>
        {
            entity.ToTable("Funciones", table =>
                table.HasCheckConstraint("CK_Funciones_Precio", "[Precio] >= 0"));

            entity.Property(f => f.FechaFuncion).HasColumnType("date");
            entity.Property(f => f.HoraInicio).HasColumnType("time(0)");
            entity.Property(f => f.Precio).HasColumnType("decimal(8,2)");
            entity.Property(f => f.Idioma).HasMaxLength(30).IsRequired();

            entity.HasOne(f => f.Pelicula)
                .WithMany(p => p.Funciones)
                .HasForeignKey(f => f.IdPelicula)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Funciones_Peliculas");

            entity.HasOne(f => f.Sala)
                .WithMany(s => s.Funciones)
                .HasForeignKey(f => f.IdSala)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Funciones_Salas");

            entity.HasIndex(f => new { f.IdSala, f.FechaFuncion, f.HoraInicio })
                .IsUnique()
                .HasDatabaseName("UQ_Funciones_Horario");
        });
    }
}