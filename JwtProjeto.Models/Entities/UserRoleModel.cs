namespace JwtProjeto.Models.Entities
{
    public class UserRoleModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public virtual RoleModel Role {get;set;}
        public virtual UserModel User {get;set;}
    }
}