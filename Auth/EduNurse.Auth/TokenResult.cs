using System;

namespace EduNurse.Auth
{
    public class TokenResult
    {
        public TokenResult(string token, DateTime expiry)
        {
            Token = token;
            Expiry = expiry;
        }

        public string Token { get; }
        public DateTime Expiry { get; }
    }
}
