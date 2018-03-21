using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyInventoryApp.Services
{
    public class LoginService : ILoginService
    {
        Dictionary<string, string> _userCredential;

        public LoginService()
        {
            _userCredential = new Dictionary<string, string>();

            _userCredential.Add("user@domain.com", "password");
        }

        public async Task<bool> Authenticate(string username, string password)
        {
            if(_userCredential.ContainsKey(username)){
                return _userCredential[username] == password;
            }

            return false;
        }
    }
}
