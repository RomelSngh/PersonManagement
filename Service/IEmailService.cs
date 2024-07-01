using PersonManagement.Model;

namespace PersonManagement.Service
{
    public interface IEmailService
    {
        Task SendEmail(Mailrequest mailrequest);
    }
}
