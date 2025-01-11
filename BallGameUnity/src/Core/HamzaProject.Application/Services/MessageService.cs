using System.Threading.Tasks;
using HamzaProject.Domain;
using HamzaProject.Application.Interfaces;

public class MessageService : IMessageService
{
    public async Task<Message> GetMessageAsync()
    {
        return new Message { Id = 1, Text = "Hamza" };
    }
} 