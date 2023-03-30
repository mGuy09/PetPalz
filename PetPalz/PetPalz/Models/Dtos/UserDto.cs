namespace PetPalz.Models.Dtos;

public class UserDto
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ProfilePicUrl { get; set; }
    public UserType UserType { get; set; }
    public ServiceType? ServiceType { get; set; }
    public Qualification? Qualification { get; set; }
    public UserRating Rating { get; set; }
    public UserYearsOfExperience? YearsOfExperience { get; set; }
    public string PhoneNumber { get; set; }
}