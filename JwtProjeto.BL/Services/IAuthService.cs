using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtProjeto.Models.Entities;

namespace JwtProjeto.BL.Services
{
    public interface IAuthService
    {
        public Task AddRefreshTokenModel(RefreshTokenModel refreshTokenModel);
        public Task<RefreshTokenModel> GetRefreshTokenModel(string refreshToken);
        public Task<UserModel> GetUserByLogin(string userName, string password);
    }
}