using System;
namespace MyInventoryApp.Api.Models.Token
{
    public class UserToken
    {
        public string IdToken { get; set; }

        public string AccessToken { get; set; }

        public int ExpiresIn { get; set; }

        public string TokenType { get; set; }

        public string RefreshTonken { get; set; }
    }
}
