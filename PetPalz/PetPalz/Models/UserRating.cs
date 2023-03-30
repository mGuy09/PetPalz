namespace PetPalz.Models;

public class UserRating: IdForEntity
{
    public string UserId { get; set; }
    public int Rating { get; set; }
}