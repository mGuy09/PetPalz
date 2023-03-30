namespace PetPalz.Models.Dtos;

public class ChatDto
{
    public int Id { get; set; }
    public string UserId1 { get; set; }
    public string UserId2 { get; set; }
    public List<ChatMessage> Messages { get; set; }
}