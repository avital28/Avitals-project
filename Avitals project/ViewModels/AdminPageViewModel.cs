using Avitals_project.Models;
using Avitals_project.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Avitals_project.ViewModels
{
    [QueryProperty(nameof(Album), "album")]
    public class AdminPageViewModel:MediaMethodsViewModel
    {
        private Album album;
        public static string albumcover;
        private string updatedcover;
        public string UpdatedCover { get { return updatedcover; } set { if (updatedcover != value) { updatedcover = value; OnPropertyChange(); } } }

        public Album Album { get { return album; } set { if (album != value) { album = value; OnPropertyChange(); } } }
        public ICommand ChangeAlbumCover { get; set; }
        public ICommand DisplayUsers { get; set; }
        public string ProfilePicture { get; set; }  

        public AdminPageViewModel(UserService service):base(service)
        {
            ProfilePicture = ((AppShell)AppShell.Current).user.ProfilePicture;
            ChangeAlbumCover = new Command(async () =>
            {
                IsOpen = false;  
                var response= await service.UpdateAlbumCover(Album,currentfile);
                UpdatedCover = album.AlbumCover;
            });
            DisplayUsers = new Command(async () =>
            {
                await Shell.Current.GoToAsync("AlbumDataPage");
            });
            Cover = "emptyalbumcover.jpg";
            IsOpen = false;
            UpdatedCover = albumcover;
        }
    }
}
