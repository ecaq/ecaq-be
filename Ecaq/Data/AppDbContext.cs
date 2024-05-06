using Ecaq.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Reflection.Metadata;

namespace Ecaq.Data;


public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }


    public DbSet<HomeBanner> HomeBanners => Set<HomeBanner>();
    public DbSet<AboutModel> AboutModels => Set<AboutModel>();
    public DbSet<EcaqCoreModel> EcaqCoreModels => Set<EcaqCoreModel>();
    public DbSet<GalleryModel> GalleryModels => Set<GalleryModel>();
    public DbSet<MemberModel> MemberModels => Set<MemberModel>();
    public DbSet<AllianceModel> AllianceModels => Set<AllianceModel>();
    public DbSet<AllianceCollectionModel> AllianceCollectionModels => Set<AllianceCollectionModel>();


    /// <summary>
    /// Sample
    /// </summary>
    public DbSet<BookModel> BookModels => Set<BookModel>();
    public DbSet<SampleProfile> SampleProfiles => Set<SampleProfile>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<AllianceModel>()
            .HasMany(a => a.AllianceCollectionModels)
            .WithOne(b => b.AllianceModel)
            .HasForeignKey(c => c.AllianceModelId);

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

