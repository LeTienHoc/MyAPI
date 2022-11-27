namespace MyAPI.Data
{
    public class RefreshToken
    {
        public Guid TokenID { get; set; }
        public string MaTk { get; set; }
        public string Token { get; set; }
        public string JwtID { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime IssuedAt { get; set; }
        public DateTime ExpiredAt { get; set; }
    }
}
