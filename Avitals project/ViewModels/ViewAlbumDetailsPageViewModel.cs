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
    public class ViewAlbumDetailsPageViewModel:ViewModel
    {
        #region private fields
        private Album album;
        private int createdat;
        #endregion
        #region public properties
        public Album Album { get { return album; } set { if (album != value) { album = value; /*PopulateList();*/ OnPropertyChange(); } } }
        //public int CreatedAt { get { return (DateTime.Now.Hour - album.CreationDate.Hour); } }
       public ObservableCollection<Media> Medias { get; set; } = new ObservableCollection<Media>();
        public ICommand SwitchColors { get; set; }
        public ICommand JoinAlbum { get; set; }
        #endregion
        #region Methods
        private async Task<List<Media>> GetMedia(Album al)
        {
            UserService s = new UserService();
            return await s.GetMediaByAlbumAsync(al);
        }
        public async void LoadMedia()
        {
            AlbumMediaPageViewModel.Media.Clear();
            List<Media> m = await GetMedia(Album);
            if (m != null)
            {
                foreach (var item in m)
                {

                    Medias.Add(item);

                }
            }
        }
        #endregion
        public ViewAlbumDetailsPageViewModel(UserService service)
        {
           

            JoinAlbum = new Command(async () =>
            {
                Album.Memebers.Add(((AppShell)Shell.Current).user);
                DisplayAlbumsPageViewModel.AllUserssAlbums.Add(Album);
                DisplayAlbumsPageViewModel.CurrentMembersAlbums.Add(Album);
                var response = await service.AddMembersAsync(Album, ((AppShell)AppShell.Current).user);
                if (response == true)
                {
                    await Shell.Current.DisplayAlert("You were added", "התחברתי", "אישור");
                    await Shell.Current.GoToAsync("AlbumMediaPage");
                }
            });
            
        }

    }
}
