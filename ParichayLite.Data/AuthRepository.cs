namespace ParichayLite.Data
{
    using ParichayLite.Domain.Entities;
    using ParichayLite.Domain.Models;
    using ParichayLite.Data;
    using Microsoft.AspNet.Identity;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ParichayLite.Domain;

    public class AuthRepository
    {
        private readonly ISqliteContext mongoContext;
       // private readonly ApplicationUserManager userManager;

        public AuthRepository(ISqliteContext mongoContext)
        {
            this.mongoContext = mongoContext;
            this.userManager = userManager;
        }

        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            var user = new User
            {
                UserName = userModel.UserName
            };

            var result = await userManager.CreateAsync(user, userModel.Password);

            return result;
        }

        public async Task<User> FindUser(string userName, string password)
        {
            User user = await userManager.FindAsync(userName, password);

            return user;
        }

        public Client FindClient(string clientId)
        {
            var query = Query<Client>.EQ(c => c.Id, clientId);

            var client = mongoContext.Clients.Find(query).SetLimit(1).FirstOrDefault();

            return client;
        }

        public async Task<bool> AddRefreshToken(RefreshToken token)
        {
            var query = Query.And(
                Query<RefreshToken>.EQ(r => r.Subject, token.Subject),
                Query<RefreshToken>.EQ(r => r.ClientId, token.ClientId));

            var existingToken = mongoContext.RefreshTokens.Find(query).SetLimit(1).SingleOrDefault();

            if (existingToken != null)
            {
                var result = await RemoveRefreshToken(existingToken);
            }

            mongoContext.RefreshTokens.Insert(token);

            return true;
        }

        public Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            var query = Query<RefreshToken>.EQ(r => r.Id, refreshTokenId);

            var writeConcernResult = mongoContext.RefreshTokens.Remove(query);

            return Task.FromResult(writeConcernResult.DocumentsAffected == 1);
        }

        public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            return await RemoveRefreshToken(refreshToken.Id);
        }

        public Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            var query = Query<RefreshToken>.EQ(r => r.Id, refreshTokenId);

            var refreshToken = mongoContext.RefreshTokens.Find(query).SetLimit(1).FirstOrDefault();

            return Task.FromResult(refreshToken);
        }

        public List<RefreshToken> GetAllRefreshTokens()
        {
            return mongoContext.RefreshTokens.FindAll().ToList();
        }
    }
}