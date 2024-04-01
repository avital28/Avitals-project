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
    public class DisplayAlbumsPageViewModel:ViewModel
    {
        #region private fields
        public static List<Album> currentusersalbums= new List<Album>();
        #endregion
        #region Properties
        public ObservableCollection<Album> Albums { get; set; }
        public string AlbumCover { get; set; }
        public ICommand ShowAlbum { get; set; }
        public Dictionary<string, object> nav = new Dictionary<string, object>();
        #endregion
        #region Methods
        private async void ShowAlbum1(Album al)
        {
            nav.Clear();
            nav.Add("album", al);
            await Shell.Current.GoToAsync("AlbumMediaPage", nav);
        }
        #endregion
        public DisplayAlbumsPageViewModel(UserService service) 
        { 
            Albums = new ObservableCollection<Album>();
            Albums.Add(new Album { AlbumTitle = "Album 1", AlbumCover = "cover1.jpg" });
            Albums.Add(new Album { AlbumTitle = "Album 2", AlbumCover = "cover2.jpg" });
            Albums.Add(new Album { AlbumTitle = "Album 3", AlbumCover = "cover3.jpg" });
            Albums.Add(new Album { AlbumTitle = "Album 4", AlbumCover = "cover4.jpg" });
            ShowAlbum = new Command<Album>(ShowAlbum1);
          

        }
    }
    }
