using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetPalz.Models;

namespace PetPalz.Data;

public class PetPalzContext:IdentityDbContext
{
    public PetPalzContext(DbContextOptions options): base(options){}

    public DbSet<Qualification> Qualifications { get; set; }
    public DbSet<UserType> UserTypes { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<ChatMessage> ChatMessages { get; set; }
    public DbSet<UserRating> UserRatings { get; set; }
    public DbSet<UserTypeInUser> UserTypesInUsers { get; set; }
    public DbSet<ProfilePicUser> ProfilePicUsers { get; set; }
    public DbSet<QualificationInUser> QualificationsInUsers { get; set;}
    

}