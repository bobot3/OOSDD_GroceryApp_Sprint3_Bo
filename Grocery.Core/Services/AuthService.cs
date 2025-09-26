using Grocery.Core.Helpers;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IClientService _clientService;

        public AuthService(IClientService clientService)
        {
            _clientService = clientService;
        }

        public Client? Login(string email, string password)
        {
            Client? client = _clientService.Get(email);
            if (client == null) return null;
            if (PasswordHelper.VerifyPassword(password, client.Password)) return client;
            return null;
        }

        public Client? Register(string email, string password)
        {
            // Check if user already exists
            if (UserExists(email))
            {
                return null; // User already exists
            }

            // Create new client (without ID - repository will assign it)
            var newClient = new Client(0, email.Split('@')[0], email, PasswordHelper.HashPassword(password));

            // Save using the client service
            _clientService.Add(newClient);

            return newClient;
        }

        public bool UserExists(string email)
        {
            return _clientService.Get(email) != null;
        }
    }
}
