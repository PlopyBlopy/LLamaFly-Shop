using Core.Interfaces;
using Core.Options;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Infrastructure.API.ProfileService.Endpoints
{
    public class ProfileApiService : IProfileService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IOptionsMonitor<ProfileServiceEndpointsOptions> _options;
        private readonly HttpClient api;

        public ProfileApiService(IHttpClientFactory httpClientFactory, IOptionsMonitor<ProfileServiceEndpointsOptions> options)
        {
            _httpClientFactory = httpClientFactory;
            _options = options;
            api = _httpClientFactory.CreateClient("ProfileServiceApi");
        }

        public async Task<bool> IsSellerExist(Guid id, CancellationToken ct)
        {
            var endpoint = _options.CurrentValue.SellerIsExist;
            var builder = new UriBuilder(api.BaseAddress)
            {
                Path = endpoint,
                Query = $"id={id}"
            };

            var response = await api.GetAsync(builder.Uri, ct);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<bool>(ct);
            }
            else
            {
                throw new HttpRequestException($"Request failed: {response.StatusCode}");
            }
        }
    }
}