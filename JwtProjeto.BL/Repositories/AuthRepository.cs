using JwtProjeto.Database.Data;
using JwtProjeto.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace JwtProjeto.BL.Repositories
{
    public class AuthRepository(AppDbContext appDbContext) : IAuthRepository
    {
        public async Task AddRefreshTokenModel(RefreshTokenModel refreshTokenModel)
        {
            await appDbContext.RefreshTokens.AddAsync(refreshTokenModel);
            await appDbContext.SaveChangesAsync();
        }

        public Task<RefreshTokenModel> GetRefreshTokenModel(string refreshToken)
        {
            return appDbContext.RefreshTokens.Include(n => n.User).ThenInclude(n => n.UserRoles).ThenInclude(n => n.Role).FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);
        }

        public Task<UserModel> GetUserByLogin(string userName, string password)
        {
            return appDbContext.Users.Include(n => n.UserRoles).ThenInclude(n => n.Role).FirstOrDefaultAsync(n => n.Username == userName && n.Password == password);
        }

        public async Task RemoveRefreshTokenByUserId(int userId)
        {
            var refreshToken = appDbContext.RefreshTokens.FirstOrDefault(n => n.UserId == userId);
            if (refreshToken is not null)
            {
                appDbContext.RemoveRange(refreshToken);
                await appDbContext.SaveChangesAsync();
            }
        }
    }
}