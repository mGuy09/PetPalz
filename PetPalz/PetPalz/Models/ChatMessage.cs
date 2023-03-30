namespace PetPalz.Models;

public class ChatMessage: IdForEntity
{
    public string SenderId { get; set; }
    public int ChatId { get; set; }
    public string Message { get; set; }
}