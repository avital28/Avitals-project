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
        private Color textcolorlabel1;
        private Color textcolorlabel2;
        private Color temp;
        #endregion
        #region public properties
        public Album Album { get { return album; } set { if (album != value) { album = value; OnPropertyChange(); } } }
        public Color TextColorLabel1 { get { return textcolorlabel1; } set { if (textcolorlabel1 != value) { textcolorlabel1 = value; OnPropertyChange(); } } }
        public Color TextColorLabel2 { get { return textcolorlabel2; } set { if (textcolorlabel2 != value) { textcolorlabel2 = value; OnPropertyChange(); } } }

        public ICommand SwitchColors { get; set; }
        public ICommand JoinAlbum { get; set; }
        #endregion

        public ViewAlbumDetailsPageViewModel(UserService service)
        {
            TextColorLabel1 = Color.FromHex("#1E7B85");
            TextColorLabel2 = Color.FromHex("#000000");
            SwitchColors = new Command(async () =>
            {
                temp = TextColorLabel1;
                TextColorLabel1 = TextColorLabel2;
                TextColorLabel2 = temp;

            });

            JoinAlbum = new Command(async () =>
            {
                Album.Memebers.Add(((AppShell)Shell.Current).user);
                await Shell.Current.GoToAsync("AlbumMediaPage");
            });
            
        }

    }
}
