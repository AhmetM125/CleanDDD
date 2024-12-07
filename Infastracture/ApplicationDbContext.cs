

using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

public sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions contextOptions)
        : base(options: contextOptions)
    {

    }
    public DbSet<Webinar> Webinars { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
      => modelBuilder.ApplyConfigurationsFromAssembly
        (assembly: typeof(ApplicationDbContext).Assembly);

    public Task<int> Commit(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}