using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vina.Server.Models;
using Microsoft.Extensions.Options;
using vina.Server.Config;

namespace vina.Server.Controllers
{
    public class AuthService
    {
        private readonly Random _random = new Random();
        private readonly string _connectionString;
        private readonly AppSettingsOptions _appSettings;
        private readonly ILogger<AuthService> _logger;
        public AuthService(ILogger<AuthService> logger, IOptions<AppSettingsOptions> appSettings, string connectionString)
        {
            _logger = logger;
            _appSettings = appSettings.Value;
            _connectionString = connectionString;
        }
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
            return new DBCustomer();
        }

        private string GenerateTempTicket()
        {
            return Guid.NewGuid().ToString();
        }
    }
}