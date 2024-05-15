using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Avitals_project.Models;
using Avitals_project.Services;


namespace Avitals_project.ViewModels
{
    [QueryProperty(nameof(nav), "Albums")]
    public class ShowAllAlbumsViewModel:ViewModel
    {
        #region private fields
        private int mediacount;
        public static ObservableCollection<Album> album = new ObservableCollection<Album>();

        public ObservableCollection<Album> Albums { get;  set; } 
        #endregion
        #region Properties
        public string AlbumCover { get; set; }
        public ICommand ShowAlbum { get; set; }
        public ICommand AddAlbum { get; set; }
        public Dictionary<string, List<object>> nav = new Dictionary<string, List<object>>();
        public Dictionary<string, object> nav2 = new Dictionary<string, object>();

        public int MediaCount { get { return mediacount; } }
        #endregion
        #region Methods
        


        private async void ShowAlbum1(Album al)
        {
            nav2.Clear();
            nav2.Add("album", al);
            await Shell.Current.GoToAsync("AlbumMediaPage", nav2);

        }
        #endregion
        public ShowAllAlbumsViewModel(UserService service)
        {
            
            Albums = new ObservableCollection<Album>(); 
            if (album.Count!=0)
            {
                foreach (Album al in album) 
                { 
                    Albums.Add(al);
                }
            }
            ShowAlbum = new Command<Album>(ShowAlbum1);
            AddAlbum = new Command(async =>
            {


            });

        }
    }
}
