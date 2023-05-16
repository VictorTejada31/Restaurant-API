namespace Restaurant.Core.Application.Dtos.Account
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsExpired => Expires <= DateTime.UtcNow;
        public DateTime Created { get; set; }
        public bool ? Revoke {  get; set; }
        public string ReplaceByToken { get; set; }
        public bool isActive => Revoke == null && IsExpired;

        
    }
}
