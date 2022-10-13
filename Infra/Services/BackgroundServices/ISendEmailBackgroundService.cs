using System.Threading.Tasks;

namespace Infra.Services.BackgroundServices
{
    public interface ISendEmailBackgroundService
    {
        Task<bool> SendEmail();
    }
}
