namespace PersonManagement.Service
{
    public interface IRefreshHandler
    {
       Task<string> GenerateToken(string username);
    }
}
