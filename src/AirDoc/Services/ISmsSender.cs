using System.Threading.Tasks;

namespace AirDoc.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
