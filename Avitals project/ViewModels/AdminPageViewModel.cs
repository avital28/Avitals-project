using Avitals_project.Models;
using Avitals_project.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Avitals_project.ViewModels
{
    [QueryProperty(nameof(Album), "album")]
    public class AdminPageViewModel : MediaMethodsViewModel
    {
        private Album album;
        public static string albumcover;
        private bool isenabled;
        private string updatedcover;
        private string updatedtitle;
        public bool IsEnabled { get { return isenabled; } set { if (isenabled != value) { isenabled = value; OnPropertyChange(); } } }
        public string UpdatedTitle { get { return updatedtitle; } set { if (updatedtitle != value) { IsEnabled = true; updatedtitle = value; OnPropertyChange(); } } }
        public string UpdatedCover { get { return updatedcover; } set { if (updatedcover != value) { updatedcover = value; OnPropertyChange(); } } }
        public Dictionary<string, object> nav = new Dictionary<string, object>();
        public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();
        public Album Album { get { return album; } set { if (album != value) { album = value; Updatedata(); OnPropertyChange(); } } }

        private void Updatedata()
        {
            UpdatedCover = albumcover;
            updatedtitle = Album.AlbumTitle;
        }
        private async void DeleteMembers(User user)
        {
            var response = await userService.DeleteMembersAsync(Album, user);
            if (response==true)
            {
                Users.Remove(user);
            }
        }

        public ICommand ChangeAlbumCover { get; set; }
        public ICommand DisplayUsers { get; set; }
        public ICommand BrowseGallery { get; set; }
        public ICommand SaveChanges { get; set; }
        public ICommand GoBack { get; set; }
        public ICommand DeleteUsers { get; set; }   
        public string ProfilePicture { get; set; }


        #region Methods
        private async void ShowAlbum1()
        {
            await userService.GetMediaByAlbumAsync(Album);
            nav.Clear();
            nav.Add("album", Album);
            await Shell.Current.GoToAsync("AlbumMediaPage", nav);
            
        }

        public async void LoadUsers()
        {
              
            UserService service = new UserService();
            var u = await service.GetMembersAsync(Album);
            if (u != null)
            {
                foreach (var item in u)
                {
                    Users.Add(item);
                }
            }

        }

        #endregion
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
            DeleteUsers = new Command<User>(DeleteMembers);
         
            GoBack = new Command(async () =>
            {
                await Shell.Current.GoToAsync("DisplayAlbumsPage");
            });
            Cover = "emptyalbumcover.jpg";
            IsOpen = false;
            IsEnabled = false;
            
            BrowseGallery = new Command(ShowAlbum1);
            SaveChanges = new Command(async () =>
            {
                var response = await service.UpdateAlbumAsync(Album.Id, UpdatedTitle);
                if (response == true)
                {
                    Album.AlbumTitle = UpdatedTitle;
                }
            });
        }
    }
}

