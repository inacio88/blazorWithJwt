namespace JwtProjeto.Models.Entities
{
    public class RefreshTokenModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string RefreshToken { get; set; }
        public virtual UserModel User {get;set;}
    }
}