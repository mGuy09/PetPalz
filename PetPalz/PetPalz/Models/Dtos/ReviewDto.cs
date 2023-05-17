namespace PetPalz.Models.Dtos;

public class ReviewDto
{
    public string UserId { get; set; }
    public string Message { get; set; }
    public int Rating { get; set; }
}