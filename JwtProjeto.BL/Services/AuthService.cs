using JwtProjeto.BL.Repositories;
using JwtProjeto.Models.Entities;

namespace JwtProjeto.BL.Services
{
    public class AuthService(IAuthRepository authRepository) : IAuthService
    {
        public async Task AddRefreshTokenModel(RefreshTokenModel refreshTokenModel)
        {
            await authRepository.RemoveRefreshTokenByUserId(refreshTokenModel.UserId);
            await authRepository.AddRefreshTokenModel(refreshTokenModel);
        }

        public Task<RefreshTokenModel> GetRefreshTokenModel(string refreshToken)
        {
            return authRepository.GetRefreshTokenModel(refreshToken);
        }

        public Task<UserModel> GetUserByLogin(string userName, string password)
        {
            return authRepository.GetUserByLogin(userName, password);
        }
    }
}