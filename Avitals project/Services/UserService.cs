﻿
using Avitals_project.Models;
using Microsoft.Maui.Layouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Avitals_project.Services
{
    public class UserService
    {
        readonly HttpClient httpClient;
        readonly JsonSerializerOptions _serializerOptions;
        const string URL = @"https://n15c5b5t-7102.uks1.devtunnels.ms/CollectiveMomentsAPI/";
        const string IMAGE_URL = @"https://n15c5b5t-7102.uks1.devtunnels.ms/images/";

        public UserService()
        {
            httpClient = new HttpClient();
            _serializerOptions = new JsonSerializerOptions() {  ReferenceHandler=ReferenceHandler.Preserve, PropertyNameCaseInsensitive = true, WriteIndented = true };
        }
        public async Task<User> LogInAsync(string username, string password)
        {
            try
            {

                User user = new User() { UserName = username, Passwrd = password };
                var jsonContent = JsonSerializer.Serialize(user);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"{URL}Login", content);

                switch (response.StatusCode)
                {
                    case (HttpStatusCode.OK):
                        {
                            jsonContent = await response.Content.ReadAsStringAsync();
                            User u = JsonSerializer.Deserialize<User>(jsonContent, _serializerOptions);
                            return u;
                        }

                    case (HttpStatusCode.Unauthorized):
                        {
                            return new UserDto() { Message = "Username or password aren't valid" };

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

        public async Task<User> UpdateUserAsync(User User)
        {
            try
            {
                User user = User;
                var jsonContent = JsonSerializer.Serialize(user);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"{URL}UpdateUser", content);
                switch (response.StatusCode)
                {
                    case (HttpStatusCode.OK):
                        {
                            jsonContent = await response.Content.ReadAsStringAsync();
                            User u = JsonSerializer.Deserialize<User>(jsonContent, _serializerOptions);
                            await Task.Delay(2000);
                            return u;
                        }
                    case (HttpStatusCode.Unauthorized):
                        {
                            return new UserDto() { Message = "Update failed" };

                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return new UserDto() { Message = "Update failed" };
        }

        public async Task<List<Album>> GetAlbumsByLocation(string longitude, string altitude)
        {
            try
            {
                Album album = new Album { Longitude = longitude, Latitude = altitude };
                var jsonContent = JsonSerializer.Serialize(album);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"{URL}GetAlbumsByLocation", content);
                switch (response.StatusCode)
                {
                    case (HttpStatusCode.OK):
                        {
                            jsonContent = await response.Content.ReadAsStringAsync();
                            List<Album> albums = JsonSerializer.Deserialize<List<Album>>(jsonContent, _serializerOptions);
                            if(albums.Count>0)
                            foreach(var al in albums)
                            {
                                    al.AlbumCover = $"{IMAGE_URL}{al.AlbumCover}";
                                    //to do run on each photo in album update photo url
                                    
                            }
                            await Task.Delay(2000);
                            return albums;
                        }
                    case (HttpStatusCode.Unauthorized):
                        {
                            return null;
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public async Task<bool> CreateAlbumAsync(Album al, FileResult file)
        {
            try
            {
                byte[] bytes;


                using (MemoryStream ms = new MemoryStream())
                {
                    var stream = await file.OpenReadAsync();
                    stream.CopyTo(ms);
                    bytes = ms.ToArray();

                    var multipartFormDataContent = new MultipartFormDataContent();

                    var imagecontent = new ByteArrayContent(bytes);
                    multipartFormDataContent.Add(imagecontent, "file", $"{file.FileName}");

                    Album album = al;
                    var jsonContent = JsonSerializer.Serialize(album);
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                    multipartFormDataContent.Add(content, "album");
                    var response = await httpClient.PostAsync($"{URL}CreateAlbum", multipartFormDataContent);
                    switch (response.StatusCode)
                    {
                        case (HttpStatusCode.OK):
                            {
                                jsonContent = await response.Content.ReadAsStringAsync();
                                Album a = JsonSerializer.Deserialize<Album>(jsonContent,_serializerOptions);
                                await Task.Delay(2000);
                                return true;
                            }
                        case (HttpStatusCode.Unauthorized):
                            {
                                return false;
                            }
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return false;


        }

        
            public async Task<List<Album>> GetAlbumsByUserAsync(int id)
            {
            try
            {
                User user= new User { Id=id };
                var jsonContent = JsonSerializer.Serialize(user);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"{URL}GetAlbumsByUser", content);
                switch (response.StatusCode)
                {
                    case (HttpStatusCode.OK):
                        {
                            jsonContent = await response.Content.ReadAsStringAsync();
                            List<Album> albums = JsonSerializer.Deserialize<List<Album>>(jsonContent, _serializerOptions);
                            await Task.Delay(2000);
                            return albums;
                        }
                    case (HttpStatusCode.Unauthorized):
                        {
                            return null;
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        //public async Task<User> UpdateAlbumCoverAsync(User User)
        //{
        //    try
        //    {
        //        User user = User;
        //        var jsonContent = JsonSerializer.Serialize(user);
        //        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        //        var response = await httpClient.PostAsync($"{URL}UpdateUser", content);
        //        switch (response.StatusCode)
        //        {
        //            case (HttpStatusCode.OK):
        //                {
        //                    jsonContent = await response.Content.ReadAsStringAsync();
        //                    User u = JsonSerializer.Deserialize<User>(jsonContent, _serializerOptions);
        //                    await Task.Delay(2000);
        //                    return u;
        //                }
        //            case (HttpStatusCode.Unauthorized):
        //                {
        //                    return new UserDto() { Message = "Update failed" };

        //                }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //    return new UserDto() { Message = "Update failed" };
        //}
    }
}

