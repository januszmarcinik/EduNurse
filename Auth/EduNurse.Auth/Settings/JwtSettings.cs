﻿namespace EduNurse.Auth.Settings
{
    internal class JwtSettings
    {
        public string Key { get; set; }
        public int ExpiryMinutes { get; set; }
        public string Issuer { get; set; }
    }
}
