using Avitals_project.Models;
using Avitals_project.Services;
using Avitals_project.Views;
using System.Text.Json;
using System.Windows.Input;

namespace Avitals_project.ViewModels
{
    public class LoginPageViewModel : ViewModel
    {
        #region Private fields
        private string username;
        private string password;
        private string errormessage;
        private bool isvisible = false;

        #endregion

        #region Properties
        public string Username { get => username; set { if (username != value) { username = value; OnPropertyChange(); } } }
        public string Password { get => password; set { if (password != value) { password = value; OnPropertyChange(); } } }
        public string ErrorMessage { get => errormessage; set { if (errormessage != value) { errormessage = value; OnPropertyChange(); } } }
        public bool IsVisible { get => isvisible; set { if (isvisible != value) { isvisible = value; OnPropertyChange(); } } }
        public ICommand LoginCommand { get; set; }
        #endregion
        //todo 
        
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
                        //TODO p : move to X screen
                        await Shell.Current.DisplayAlert("success", "Logged in", "Done");
                        isvisible = true;
                        var useralbums= await service.GetAlbumsByUserAsync(user);
                        foreach(var album in useralbums)
                        {
                            DisplayAlbumsPageViewModel.AllUserssAlbums.Add(album);
                           if (album.AdminId==user.Id)
                                DisplayAlbumsPageViewModel.CurrentAdminsAlbums.Add(album);
                           else
                                DisplayAlbumsPageViewModel.CurrentMembersAlbums.Add(album);


                        }
                        await Shell.Current.GoToAsync("///UserDetailsPage");

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
