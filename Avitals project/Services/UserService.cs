
using Avitals_project.Models;
using Microsoft.Maui.Layouts;
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
        //readonly JsonSerializerOptions _serializerOptions;
        const string URL = @"https://sg0xhwpv-7102.euw.devtunnels.ms/CollectiveMomentsAPI/";

        public UserService ()
        {
            httpClient = new HttpClient();
        }
        public async Task<User> LogInAsync(string username, string password)
        {
            try
            {
                
                User user = new User() { Username = username, Passwrd = password };
                var jsonContent = JsonSerializer.Serialize(user);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"{URL}Login", content);

                switch (response.StatusCode)
                {
                    case (HttpStatusCode.OK):
                        {
                            jsonContent = await response.Content.ReadAsStringAsync();
                            User u = JsonSerializer.Deserialize<User>(jsonContent);
                            return user;
                        }
                        
                    case (HttpStatusCode.Unauthorized):
                        {
                            return new UserDto() { Message="Username or password aren't valid"};
                            
                        }
                        

                }

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return new UserDto() { User = null, Message = "Username or password aren't valid" };

        }

        public async Task<bool> RegisterAsync(User User)
        {
            try
            {
                User user = User;
                var jsonContent = JsonSerializer.Serialize(user);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"{URL}Register", content);
                switch (response.StatusCode)
                {
                    case (HttpStatusCode.OK):
                        {
                            jsonContent = await response.Content.ReadAsStringAsync();
                            User u = JsonSerializer.Deserialize<User>(jsonContent);
                            await Task.Delay(2000);
                            return true;
                        }
                    case (HttpStatusCode.Unauthorized):
                        {
                            return false;
                        }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return false;
        }
    }
}
