using JwtProjeto.Models.Entities;

namespace JwtProjeto.BL.Repositories
{
    public interface IAuthRepository
    {
        public Task AddRefreshTokenModel(RefreshTokenModel refreshTokenModel);
        public Task<RefreshTokenModel> GetRefreshTokenModel(string refreshToken);
        public Task<UserModel> GetUserByLogin(string userName, string password);
        public Task RemoveRefreshTokenByUserId(int userId);
    }
}