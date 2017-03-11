using Entities;

namespace BusinessService
{
    public interface IUserServices
    {
        int Authenticate(string userName, string password);
        TokenEntity GenerateToken(int userId);
        bool ValidateToken(string tokenId);
        bool Kill(string tokenId);
        bool DeleteByUserId(string userId);
    }
}
