using Core.Services.InterfaceServices;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class UserCommunicationService : IUserService
    {
        private readonly HttpClient httpClient;
        private const string URL_GET_USER = "/Users/users/";

        #region Constructor
        public UserCommunicationService(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }
        #endregion

        private static readonly IAsyncPolicy<HttpResponseMessage> _circuitBreakerPolicy = Policy
        .Handle<HttpRequestException>()
        .OrResult<HttpResponseMessage>(response => !response.IsSuccessStatusCode)
        .CircuitBreakerAsync(5, TimeSpan.FromMinutes(2));

        public async Task<bool> UserExists(long userId)
        {
            try
            {
                HttpResponseMessage response = await _circuitBreakerPolicy.ExecuteAsync(() =>  httpClient.GetAsync($"{URL_GET_USER}{userId}"));
                return response.IsSuccessStatusCode;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
