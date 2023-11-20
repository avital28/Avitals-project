using Avitals_project.Models;
using System.Text.Json;

namespace Avitals_project
{
    public partial class AppShell : Shell
    {
       public User user { get; set; }
        public AppShell()
        {
            InitializeComponent();

        }
        protected override async void OnAppearing()
        {
             var content = await SecureStorage.Default.GetAsync("user");
            user= JsonSerializer.Deserialize<User>(content);

            base.OnAppearing();
        }
    }
}