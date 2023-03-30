namespace PetPalz.Models;

public class QualificationInUser:IdForEntity
{
    public string UserId { get; set; }
    public int QualificationId { get; set; }
}