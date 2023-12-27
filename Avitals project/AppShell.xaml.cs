using Avitals_project.Models;
using Avitals_project.Views;
using System.Text.Json;

namespace Avitals_project
{
    public partial class AppShell : Shell
    {
       public User user { get; set; }
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("Login", typeof(LoginPage));
            Routing.RegisterRoute("RegisterPage", typeof(RegisterPage));    
            Routing.RegisterRoute("UserDetailsPage", typeof(UserDetailsPage));
            Routing.RegisterRoute("SettingsPage", typeof(SettingsPage));
        }
        protected override async void OnAppearing()
        {
             var content = await SecureStorage.Default.GetAsync("user");
            if (content!=null)
            user= JsonSerializer.Deserialize<User>(content);
            base.OnAppearing();
            //todo if user exists go to main page
        }
    }
}