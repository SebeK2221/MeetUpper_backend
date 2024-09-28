using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class MeetUpperDbContext:IdentityDbContext<User>
{
    public MeetUpperDbContext(DbContextOptions<MeetUpperDbContext>options): base(options){}
    public DbSet<Event> Events { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Event>()
            .HasIndex(e => e.Name)
            .IsUnique();
    }
}