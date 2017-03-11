using DataAccess;
using Entities;
using System;
using System.Configuration;
using System.Linq;


namespace BusinessService
{
    public class UserService : IUserServices
    {
        public int Authenticate(string userName, string password)
        {
            var user = (new UserRepository()).Authenticate(userName, password);
            if (user != null && user.UserId > 0)
            {
                return user.UserId;
            }
            return 0;
        }
        
        public TokenEntity GenerateToken(int userId)
        {
            string token = Guid.NewGuid().ToString();
            DateTime issuedOn = DateTime.Now;
            DateTime expiredOn = DateTime.Now.AddSeconds(Convert.ToDouble(ConfigurationManager.AppSettings["AuthTokenExpiry"]));
            var tokendomain = new TokenEntity
            {
                UserId = userId,
                AuthToken = token,
                IssuedOn = issuedOn,
                ExpiresOn = expiredOn
            };

            var a = new UserRepository().InsertToken(tokendomain);

            var tokenModel = new TokenEntity()
            {
                UserId = userId,
                IssuedOn = issuedOn,
                ExpiresOn = expiredOn,
                AuthToken = token
            };

            return tokenModel;
        }

        public bool ValidateToken(string tokenId)
        {
            var token = new UserRepository().GetToken(tokenId).FirstOrDefault();
            if (token != null && !(DateTime.Now > token.ExpiresOn))
            {
                token.ExpiresOn = token.ExpiresOn.AddSeconds(
                                              Convert.ToDouble(ConfigurationManager.AppSettings["AuthTokenExpiry"]));
                new UserRepository().UpdateToken(token);
                return true;
            }
            return false;
        }

        public bool Kill(string tokenId)
        {
            new UserRepository().DeleteToken(tokenId);
            var isNotDeleted = new UserRepository().GetToken(tokenId).Any();
            if (isNotDeleted) { return false; }
            return true;
        }

        public bool DeleteByUserId(string userId)
        {
            new UserRepository().DeleteToken(userId);
            
            var isNotDeleted = new UserRepository().GetToken(userId).Any();
            return !isNotDeleted;
        }
    }
}