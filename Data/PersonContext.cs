using Microsoft.EntityFrameworkCore;
using PersonInterestApi.Models;

namespace PersonInterestApi.Data;

public class PersonContext : DbContext
{
    public PersonContext(DbContextOptions<PersonContext> options) : base(options) { }

    public DbSet<Person> People { get; set; }
    public DbSet<Interest> Interests { get; set; }
    public DbSet<Link> Links { get; set; }
    public DbSet<PersonInterest> PersonInterests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PersonInterest>()
            .HasKey(pi => new { pi.PersonId, pi.InterestId });

        modelBuilder.Entity<PersonInterest>()
            .HasOne(pi => pi.Person)
            .WithMany(p => p.PersonInterests)
            .HasForeignKey(pi => pi.PersonId);

        modelBuilder.Entity<PersonInterest>()
            .HasOne(pi => pi.Interest)
            .WithMany(i => i.PersonInterests)
            .HasForeignKey(pi => pi.InterestId);
    }
}
