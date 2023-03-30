namespace PetPalz.Models;

public class ServiceTypeInUser:IdForEntity
{
    public string UserId { get; set; }
    public int ServiceTypeId { get; set; }
}