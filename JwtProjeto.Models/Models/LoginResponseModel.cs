using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtProjeto.Models.Models
{
    public class LoginResponseModel
    {
        public string Token { get; set; }
        public long TokenExpired { get; set; }
    }
}