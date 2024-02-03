using Ecaq.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Ecaq.Data;


public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }


    public DbSet<BookModel> BookModels => Set<BookModel>();
    public DbSet<SampleProfile> SampleProfiles => Set<SampleProfile>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


        //modelBuilder.Entity<ApplicationUser>()
        //    .HasMany(p => p.Roles).WithOne()
        //    .HasForeignKey(p => p.UserId)
        //    .IsRequired()
        //    .OnDelete(DeleteBehavior.Cascade);

        //modelBuilder.Entity<ApplicationUser>()
        //    .HasMany(e => e.Claims)
        //    .WithOne().HasForeignKey(e => e.UserId)
        //    .IsRequired()
        //    .OnDelete(DeleteBehavior.Cascade);

        //modelBuilder.Entity<ApplicationRole>()
        //    .HasMany(r => r.Claims).WithOne()
        //    .HasForeignKey(r => r.RoleId)
        //    .IsRequired()
        //    .OnDelete(DeleteBehavior.Cascade);



        //// Used in Article Entity for string[]
        //var converter = new ValueConverter<string[], string>(
        //        v => string.Join(";", v),
        //        v => v.Split(";", StringSplitOptions.RemoveEmptyEntries).Select(val => val).ToArray());
        //modelBuilder.Entity<AppUser>()
        //    .Property(e => e.AssignedRoles)
        //    .HasConversion(converter!);
        //modelBuilder.Entity<Product>()
        //    .Property(e => e.TagList)
        //    .HasConversion(converter!);
        //// Used in Article Entity for string[]

    }

}

