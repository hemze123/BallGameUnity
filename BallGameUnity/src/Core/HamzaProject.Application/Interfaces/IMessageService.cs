namespace HamzaProject.Application.Interfaces
{
    public interface IMessageService
    {
        Task<Message> GetMessageAsync();
    }
} 