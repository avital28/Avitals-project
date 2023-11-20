using Avitals_project.Models;
using Avitals_project.Services;
using System.Text.Json;
using System.Windows.Input;

namespace Avitals_project.ViewModels
{
    public class LoginPageViewModel : ViewModel
    {
        private string username;
        private string password;
        private string errormessage;
        public string Username { get => username; set { if (username != value) { username = value; OnPropertyChange(); } } }
        public string Password { get => password; set { if (password != value) { password = value; OnPropertyChange(); } } }
        public string ErrorMessage { get => errormessage; set { if (errormessage != value) { errormessage = value; OnPropertyChange(); } } }

        public ICommand LoginCommand { get; set; }
        public LoginPageViewModel(UserService service)
        {
            LoginCommand = new Command(async () =>
            {
                try
                {
                    var user = await service.LogInAsync(username, password);
                    if (user is UserDto)
                    {
                        ErrorMessage = ((UserDto)user).Message;
                    }
                    else
                    {
                        ((AppShell)AppShell.Current).user = user;
                        await SecureStorage.Default.SetAsync("user", JsonSerializer.Serialize(user));
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            });
        }

        
    }
}
