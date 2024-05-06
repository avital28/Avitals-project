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
    public class ViewAlbumDetailsPageViewModel:ViewModel
    {
        #region private fields
        private Album album;
        private int createdat;
        #endregion
        #region public properties
        public Album Album { get { return album; } set { if (album != value) { album = value; OnPropertyChange(); } } }
        public int CreatedAt { get { return (DateTime.Now.Hour - album.CreationDate.Hour); } }
       
        public ICommand SwitchColors { get; set; }
        public ICommand JoinAlbum { get; set; }
        #endregion

        public ViewAlbumDetailsPageViewModel(UserService service)
        {
           

            JoinAlbum = new Command(async () =>
            {
                Album.Memebers.Add(((AppShell)Shell.Current).user);
                await Shell.Current.GoToAsync("AlbumMediaPage");
            });
            
        }

    }
}
