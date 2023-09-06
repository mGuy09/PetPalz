using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetPalz.Models;

namespace PetPalz.Data;

public class PetPalzContext : IdentityDbContext
{
    public PetPalzContext( DbContextOptions options ) : base(options) { }

    public DbSet<Qualification> Qualifications { get; set; }
    public DbSet<UserType> UserTypes { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<ChatMessage> ChatMessages { get; set; }
    public DbSet<UserRating> UserRatings { get; set; }
    public DbSet<UserTypeInUser> UserTypesInUsers { get; set; }
    public DbSet<ProfilePicUser> ProfilePicUsers { get; set; }
    public DbSet<QualificationInUser> QualificationsInUsers { get; set; }
    public DbSet<ServiceType> ServiceTypes { get; set; }
    public DbSet<ServiceTypeInUser> ServiceTypeInUsers { get; set; }
    public DbSet<UserYearsOfExperience> UserYearsOfExperience { get; set;}
    public DbSet<UserFullName> UserFullNames { get; set; }
    public DbSet<UserDescription> UserDescriptions { get; set; }
    public DbSet<UserStatus> UserStatuses { get; set; }
    public DbSet<UserReviews> UserReviews { get; set; }

    //protected override void OnModelCreating( ModelBuilder builder )
    //{
    //    base.OnModelCreating(builder);
    //    SeedRoles(builder);
    //}
    //private static void SeedRoles( ModelBuilder builder )
    //{
    //    builder.Entity<IdentityRole>().HasData(
    //        new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "ADMIN" },
    //        new IdentityRole() { Name = "petOwner", ConcurrencyStamp = "2", NormalizedName = "PETOWNER" },
    //        new IdentityRole() { Name = "petSitter", ConcurrencyStamp = "3", NormalizedName = "PETSITTER" }
    //    );
    //}
}