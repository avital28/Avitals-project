using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avitals_project.Models;
using System.Text.Json;
namespace Avitals_project.ViewModels
{
    public class UserDetailsPageViewModel:ViewModel
    {
        private  User user = ((AppShell)Shell.Current).user;
        private string username;
        public string Username { get => username; set { if (username != value) { username = value; OnPropertyChange(); } } }
        #region Get current user
        public static async Task<User> GetUserAsync()
        {
            User user=new User();
            var content = await SecureStorage.Default.GetAsync("user");
            if (content != null)
                user = JsonSerializer.Deserialize<User>(content);
            return user;
        }
        #endregion
        public UserDetailsPageViewModel()
        {
            if (user != null)
            {
                //default values
            }
        }
    }
}
