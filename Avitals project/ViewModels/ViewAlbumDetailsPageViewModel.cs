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
        public Album Album { get { return album; } set { if (album != value) { album = value; OnPropertyChange(); PopulateMedia(); } } }
       
       public ObservableCollection<Media> Medias { get; set; } = new ObservableCollection<Media>();
        public ICommand JoinAlbum { get; set; }
        #endregion
        #region Methods
      private async Task PopulateMedia()
        {
            
            if (Album.Media.Count>0)
            {
                foreach (var item in Album.Media)
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
