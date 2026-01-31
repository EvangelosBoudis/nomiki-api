using Microsoft.EntityFrameworkCore;
using Nomiki.Api.InterestRate.Domain;

namespace Nomiki.Api.InterestRate.Database;

/// <summary>
/// The primary database context for the application, responsible for managing 
/// <see cref="InterestRateDefinition"/> entities and PostgreSQL connectivity.
/// </summary>
/// <param name="options">The options to be used by this <see cref="DbContext"/>.</param>
public class DataContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<InterestRateDefinition>(entity =>
        {
            entity.HasKey(i => i.Id);

            entity.Property(i => i.AdministrativeAct).HasMaxLength(100).IsRequired();
            entity.Property(i => i.Fek).HasMaxLength(100).IsRequired();
            entity.Property(i => i.ContractualRate).HasPrecision(18, 2).IsRequired();
            entity.Property(i => i.DefaultRate).HasPrecision(18, 2).IsRequired();
            entity.Property(i => i.DeterministicHash).HasMaxLength(200).IsRequired();

            entity.HasIndex(r => new { r.From, r.To }).IsUnique();
        });
    }
}