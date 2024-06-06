
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

        public async Task<List<Album>> GetAlbumsByLocation(string longitude, string latitude)
        {
            try
            {
                
                var response = await httpClient.GetAsync($"{URL}GetAlbumsByLocation?longitude={longitude}&latitude={latitude}&id={((AppShell)Shell.Current).user.Id}");
                switch (response.StatusCode)
                {
                    case (HttpStatusCode.OK):
                        {
                            var jsonContent = await response.Content.ReadAsStringAsync();
                            List<Album> albums = JsonSerializer.Deserialize<List<Album>>(jsonContent, _serializerOptions);
                            if(albums.Count>0)
                            foreach(var al in albums)
                            {
                                    al.AlbumCover = $"{IMAGE_URL}{al.AlbumCover}";
                                    
                                    
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
        public async Task<List<Media>> GetMediaByAlbumm(int id)
        {
            try
            {
                Album album = new Album { Id=id };
                var jsonContent = JsonSerializer.Serialize(album);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"{URL}GetMediaByAlbum", content);
                switch (response.StatusCode)
                {
                    case (HttpStatusCode.OK):
                        {
                            jsonContent = await response.Content.ReadAsStringAsync();
                            List<Media> albummedia = JsonSerializer.Deserialize<List<Media>>(jsonContent, _serializerOptions);
                            if (albummedia.Count > 0)
                                foreach (var al in albummedia)
                                {
                                    al.Sources = $"{IMAGE_URL}{al.Sources}";
                                    //to do run on each photo in album update photo url

                                }
                            await Task.Delay(2000);
                            return albummedia;
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
                                al.Id = a.Id;   
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

        public async Task<bool> UpadAsync(Album al, FileResult file)
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
                                Album a = JsonSerializer.Deserialize<Album>(jsonContent, _serializerOptions);
                                al.Id = a.Id;
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
        public async Task<List<Album>> GetAlbumsByUserAsync(User u)
            {
            try
            {
                
                
                var response = await httpClient.GetAsync($"{URL}GetAlbumsByUser?userId={u.Id}");
                switch (response.StatusCode)
                {
                    case (HttpStatusCode.OK):
                        {
                           var jsonContent = await response.Content.ReadAsStringAsync();
                            List<Album> albums = JsonSerializer.Deserialize<List<Album>>(jsonContent, _serializerOptions);
                            if (albums!=null)
                            {
                                foreach (var al in albums)
                                {
                                    al.AlbumCover = $"{IMAGE_URL}{al.AlbumCover}";
                                    if (al.Media.Count > 0)
                                    {
                                        foreach (var item in al.Media)
                                        {
                                            item.Sources  = $"{IMAGE_URL}{item.Sources}";

                                        }
                                    }

                                }
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
        public async Task<string> UploadMedia(Album al, Media media, FileResult file)
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
                    var jsonContent2 = JsonSerializer.Serialize(media);
                    var content2 = new StringContent(jsonContent2, Encoding.UTF8, "application/json");
                    multipartFormDataContent.Add(content, "album");
                    multipartFormDataContent.Add(content2, "photo");
                    var response = await httpClient.PostAsync($"{URL}UploadMedia", multipartFormDataContent);
                    switch (response.StatusCode)
                    {
                        case (HttpStatusCode.OK):
                            {
                                jsonContent = await response.Content.ReadAsStringAsync();
                                Album a = JsonSerializer.Deserialize<Album>(jsonContent, _serializerOptions);
                                await Task.Delay(2000);
                                a.Media.Last().Sources = $"{IMAGE_URL}{a.Media.Last().Sources}";
                                return a.Media.Last().Sources;


                            }
                        case (HttpStatusCode.Unauthorized):
                            {
                                return null;
                            }
                    }
                
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return null;
        }

        public async Task<bool> GetMediaByAlbumAsync(Album a)
        {
            try
            {
               
                var response = await httpClient.GetAsync($"{URL}GetMediaByAlbum?albumId={a.Id}");
                switch (response.StatusCode)
                {
                    case (HttpStatusCode.OK):
                        {
                            var jsonContent = await response.Content.ReadAsStringAsync();
                            List<Media> m = JsonSerializer.Deserialize<List<Media>>(jsonContent, _serializerOptions);
                            a.Media.Clear();
                            foreach (var item in m)
                            {
                                item.Sources = $"{IMAGE_URL}{item.Sources}";
                                a.Media.Add(item);
                            }
                            
                            await Task.Delay(2000);
                            return true;
                        }
                    case (HttpStatusCode.Unauthorized):
                        {
                            return false;
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
        public async Task<List<User>> GetMembersAsync (Album a) 
        {
            try
            {

                var response = await httpClient.GetAsync($"{URL}GetMembersByAlbum?albumId={a.Id}");
                switch (response.StatusCode)
                {
                    case (HttpStatusCode.OK):
                        {
                            var jsonContent = await response.Content.ReadAsStringAsync();
                            List<User> U = JsonSerializer.Deserialize<List<User>>(jsonContent, _serializerOptions);
                            
                            await Task.Delay(2000);
                            return U;
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

        public async Task<bool> DeleteMembersAsync(Album a, User u)
        {
            try
            {

                var response = await httpClient.DeleteAsync($"{URL}GetMembersByAlbum?albumId={a.Id}&userId={u.Id}");
                switch (response.StatusCode)
                {
                    case (HttpStatusCode.OK):
                        {
                            var jsonContent = await response.Content.ReadAsStringAsync();
                            bool Success = JsonSerializer.Deserialize<bool>(jsonContent, _serializerOptions);
                            if (Success==true) 
                            {
                                a.Memebers.Remove(u);
                            }
                            await Task.Delay(2000);
                            return Success;
                        }
                    case (HttpStatusCode.Unauthorized):
                        {
                            return false;
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
        public async Task<bool> AddMembersAsync(Album a, User user)
        {
            try
            {

                var multipartFormDataContent = new MultipartFormDataContent();

               

               
                //var jsonContent = JsonSerializer.Serialize(a);
                //var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                //var jsonContent2 = JsonSerializer.Serialize(user);
                //var content2 = new StringContent(jsonContent2, Encoding.UTF8, "application/json");
                //multipartFormDataContent.Add(content, "album");
                //multipartFormDataContent.Add(content2, "user");
                var response = await httpClient.PostAsync($"{URL}AddMember?albumId={a.Id}&&userId={user.Id}", null);
                switch (response.StatusCode)
                {

                    case (HttpStatusCode.OK):
                        {
                             var jsonContent = await response.Content.ReadAsStringAsync();
                            var r= JsonSerializer.Deserialize<bool>(jsonContent, _serializerOptions);

                            await Task.Delay(2000);
                            return r;
                        }
                    case (HttpStatusCode.Unauthorized):
                        {
                            return false;
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
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

        public async Task<bool> UpdateAlbumCover(Album al, FileResult file)
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
                    var response = await httpClient.PostAsync($"{URL}UpdateAlbumCover", multipartFormDataContent);
                    switch (response.StatusCode)
                    {
                        case (HttpStatusCode.OK):
                            {
                                jsonContent = await response.Content.ReadAsStringAsync();
                               
                                await Task.Delay(2000);
                                al.AlbumCover = $"{IMAGE_URL}{jsonContent}";
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

        public async Task<bool> UpdateAlbumAsync(int id, string title)
        {
            try
            {

                Album al = new Album() { Id = id, AlbumTitle = title };
                var jsonContent = JsonSerializer.Serialize(al);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"{URL}UpdateAlbum", content);
                switch (response.StatusCode)
                {
                    case (HttpStatusCode.OK):
                        {
                            jsonContent = await response.Content.ReadAsStringAsync();
                            var IsUpdated = JsonSerializer.Deserialize<bool>(jsonContent, _serializerOptions);
                            await Task.Delay(2000);
                            return IsUpdated;
                        }
                    case (HttpStatusCode.Forbidden):
                        {
                            return false;
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public async Task<bool> AddMember(Album al, User user)
        {
            try
            {


                var multipartFormDataContent = new MultipartFormDataContent();
                Album album = al;
                var jsonContent = JsonSerializer.Serialize(album);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                multipartFormDataContent.Add(content, "album");
                var jsonContent2 = JsonSerializer.Serialize(album);
                var content2 = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                multipartFormDataContent.Add(content2, "user");
                var response = await httpClient.PostAsync($"{URL}AddMember", multipartFormDataContent);
                switch (response.StatusCode)
                {
                    case (HttpStatusCode.OK):
                        {
                            jsonContent = await response.Content.ReadAsStringAsync();

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


