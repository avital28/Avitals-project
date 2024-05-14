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
            Routing.RegisterRoute("AddAlbumPage", typeof(AddAlbumPage));
            Routing.RegisterRoute("CreateAlbumPage", typeof(CreateAlbumPage));
            Routing.RegisterRoute("DisplayAlbumsPage", typeof(DisplayAlbumsPage));
            Routing.RegisterRoute("AlbumMediaPage", typeof (AlbumMediaPage));
            Routing.RegisterRoute("ViewAlbumDetailsPage", typeof(ViewAlbumDetailsPage));
            Routing.RegisterRoute("ShowAllAlbums", typeof(ShowAllAlbums));
            Routing.RegisterRoute("AdminPage", typeof(AdminPage));
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
             var content = await SecureStorage.Default.GetAsync("user");
            if (content!=null)
            user= JsonSerializer.Deserialize<User>(content);
            //todo if user exists go to main page
        }
    }
}