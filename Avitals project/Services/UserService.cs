using ABI.System;
using Avitals_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Avitals_project.Services
{
    public class UserService
    {
        readonly HttpClient httpClient;
        readonly JsonSerializerOptions _serializerOptions;
        const string URL = @"";

        public UserService (HttpClient client)
        {
            httpClient = client;
        }
        public async Task<User> LogInAsync(string userName, string password)
        {
            try
            {
                
                User user = new User() { Username = userName, Password = password };
                var jsonContent = JsonSerializer.Serialize(user, _serializerOptions);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"{URL}Login", content);

                switch (response.StatusCode)
                {
                    case (HttpStatusCode.OK):
                        {
                            jsonContent = await response.Content.ReadAsStringAsync();
                            User u = JsonSerializer.Deserialize<User>(jsonContent, _serializerOptions);
                            await Task.Delay(2000);
                            break;
                        }
                        
                    case (HttpStatusCode.Unauthorized):
                        {
                            return new UserDto() { Success = false, User = null, Message = ErrorMessages.INVALID_LOGIN };
                            break;
                        }
                        

                }

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return new UserDto() { Success = false, User = null, Message = ErrorMessages.INVALID_LOGIN };

        }
    }
}
