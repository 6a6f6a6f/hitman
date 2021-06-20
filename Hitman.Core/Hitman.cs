using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Net.Http.Json;
using System.Threading.Tasks;

using Hitman.Core.Records;

namespace Hitman.Core
{
    /// <summary>
    /// Entrypoint for <see cref="Hitman"/> scraping framework.
    /// </summary>
    public class Hitman : IDisposable
    {
        private readonly SemaphoreSlim _semaphoreSlim;

        private readonly HttpClientHandler _httpClientHandler;
        private readonly CookieContainer _cookieContainer;
        private readonly HttpClient _httpClient;
        private readonly Session _session;

        /// <summary>
        /// Default constructor for <see cref="Hitman"/>.
        /// <para>This hole class is parallel and concurrent by default.</para>
        /// </summary>
        public Hitman(Session session)
        {
            _session = session;

            _cookieContainer = new CookieContainer();
            _httpClientHandler = new HttpClientHandler
            {
                AllowAutoRedirect = false,
                UseCookies = true,
                CookieContainer = _cookieContainer
            };
            _httpClient = new HttpClient(_httpClientHandler)
            {
                DefaultRequestHeaders =
                {
                    {"User-Agent", Constants.DefaultUserAgent},
                    {"CSRF-Token", session.Jsessionid},
                    {"Accept", "application/json"},
                }
            };
            
            _semaphoreSlim = new SemaphoreSlim(Environment.ProcessorCount);

            _cookieContainer
                .Add(new Cookie("JSESSIONID", session.Jsessionid, "/", ".www.linkedin.com"));

            _cookieContainer
                .Add(_session.LiAt.Contains("\"")
                    ? new Cookie("li_at", session.LiAt, "/", ".www.linkedin.com")
                    : new Cookie("li_at", $"\"{session.LiAt}\"", "/", ".www.linkedin.com"));
        }

        /// <summary>
        /// Validate if current <see cref="Session"/> is valid.
        /// </summary>
        /// <returns>If networkinfo endpoint does return 200 status code on response.</returns>
        public async Task<bool> IsSessionValidAsync()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get,
                $"https://www.linkedin.com/voyager/api/identity/profiles/{_session.Handle}/networkinfo");

            var response = await _httpClient.SendAsync(requestMessage);
            return response.IsSuccessStatusCode;
        }

        public async Task<NetworkInformation> GetNetworkInformationFromHandle(string handle)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get,
                $"https://www.linkedin.com/voyager/api/identity/profiles/{handle}/networkinfo");

            var response = await _httpClient.SendAsync(requestMessage);
            var content = await response.Content.ReadFromJsonAsync<NetworkInformation>();

            return content;
        }
        
        public void Dispose()
        {
            _httpClient?.Dispose();
            _httpClientHandler?.Dispose();

            _semaphoreSlim?.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}

