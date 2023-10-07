using Core.Services.InterfaceServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class UserCommunicationService : IUserService
    {
        private readonly HttpClient httpClient;
        private const string URL_GET_USER = "/Users/users/";
        public UserCommunicationService(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }

        public async Task<bool> UserExists(long userId)
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync($"{URL_GET_USER}{userId}");
                return response.IsSuccessStatusCode;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
