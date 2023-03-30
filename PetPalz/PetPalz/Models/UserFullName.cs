namespace PetPalz.Models;

public class UserFullName:IdForEntity
{
    public string UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}