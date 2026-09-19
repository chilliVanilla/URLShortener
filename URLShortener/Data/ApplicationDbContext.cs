using Microsoft.EntityFrameworkCore;
using URLShortener.Models;

namespace URLShortener.Data;

public class ApplicationDbContext:DbContext
{
    public ApplicationDbContext( DbContextOptions<ApplicationDbContext> options): base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelbuilder)
    {
        base.OnModelCreating(modelbuilder);
        
        modelbuilder.Entity<ShortLink>(entity =>
        {
            entity.Property(x=>x.Code).HasMaxLength(20);
            entity.Property(x => x.OriginalUrl).HasMaxLength(2048);
            entity.HasIndex(x => x.Code).IsUnique();
        }
            );
    }

    public DbSet<ShortLink> ShortLinks { get; set; }
}