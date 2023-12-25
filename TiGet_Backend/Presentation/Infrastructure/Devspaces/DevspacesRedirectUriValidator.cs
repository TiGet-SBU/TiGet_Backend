using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;


namespace Rhazes.Services.Identity.API.Infrastructure.Devspaces
{
    public class DevspacesRedirectUriValidator : IRedirectUriValidator
    {
        private readonly ILogger _logger;
        public DevspacesRedirectUriValidator(ILogger<DevspacesRedirectUriValidator> logger)
        {
            _logger = logger;
        }

        public Task<bool> IsPostLogoutRedirectUriValidAsync(string requestedUri, Client client)
        {

            _logger.LogInformation($"Client {client.ClientName} used post logout uri {requestedUri}.");
            return Task.FromResult(true);
        }

        public Task<bool> IsRedirectUriValidAsync(string requestedUri, Client client)
        {
            _logger.LogInformation($"Client {client.ClientName} used redirect uri {requestedUri}.");
            return Task.FromResult(true);
        }

    }
}