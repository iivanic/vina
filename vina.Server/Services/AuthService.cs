using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vina.Server.Models;

namespace vina.Server.Controllers
{
    public class AuthService
    {
         private readonly Random _random = new Random();

        public string GenerateAuthLink(string email)
        {
            var token = Guid.NewGuid().ToString();
            var user = new DBCustomer
            {
                Email = email
                
            };

            // In a real application, you would send the link via email
            return $"https://yourapp.com/auth/confirm?token={token}";
        }

        public DBCustomer ValidateToken(string token)
        {
           // if (_userStore.TryGetValue(token, out var user))
          //  {
         //       user.TempTicket = GenerateTempTicket();
          //      return user;
          //  }
            return null;
        }

        private string GenerateTempTicket()
        {
            return Guid.NewGuid().ToString();
        }
    }
}