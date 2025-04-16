using System.Reflection;
using Concierge.Models;
using Microsoft.EntityFrameworkCore;

namespace Concierge.Data;

/// <summary>
/// The Application Database Context.
/// </summary>
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        // empty
    }

    /// <summary>
    /// The Personas DbSet.
    /// </summary>
    public DbSet<Persona> Personas => Set<Persona>();

    /// <summary>
    /// OnModelCreating method to configure the model and seed the database.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Apply all configurations from assembly
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Configure the entities
        modelBuilder.Entity<Persona>();

        // Seed the database with initial data
        modelBuilder.Entity<Persona>().HasData(
            new Persona { Nombre = "Diego", Apellidos = "Urrutia-Astorga", Email = "durrutia@ucn.cl", Rut = "130144918" }
        );
    }
}