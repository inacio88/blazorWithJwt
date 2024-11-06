using System.Security.Cryptography.X509Certificates;

namespace JwtProjeto.Models.Entities
{
    public class UserModel
    {
        public UserModel()
        {
            UserRoles = new List<UserRoleModel>();
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public virtual ICollection<UserRoleModel> UserRoles {get;set;}
    }
}