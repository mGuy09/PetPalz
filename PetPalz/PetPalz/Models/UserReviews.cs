namespace PetPalz.Models;

public class UserReviews:BaseEntity
{
    public string UserId { get; set; }

    public string PostUserId { get; set; }
    public int Rating { get; set; }
}