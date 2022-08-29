using System;
using System.Text;
using System.Text.Json;
using webapi_yotam.DTO;

namespace webapi_yotam.SyncDataServices.Http
{
    public class HttpCommandDataClient: ICommandDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public HttpCommandDataClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task SendPlatformToCommand(PlatformReadDTO plat)
        {
            var addres=_config["CommandService"];
            var httpContent = new StringContent(
                JsonSerializer.Serialize(plat),
                Encoding.UTF8,
                "application/json");

            

            var respone = await _httpClient.PostAsync(addres,httpContent);
            if (respone.IsSuccessStatusCode)
            {
                Console.WriteLine("---> the connection is ok");
            }
            else
            {
                Console.WriteLine("--> connection not ok");
            }

        }
    }
}

