using System;

namespace ChattyAPI.Authorization.DTO
{
    public class LoginResultDTO
    {
        public Guid gd { get; set; }
        public object token { get; set; }
    }
}