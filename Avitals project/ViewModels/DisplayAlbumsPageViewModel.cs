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
        #endregion

        public DisplayAlbumsPageViewModel(UserService service) 
        { 
            Albums = new ObservableCollection<Album>();
            Albums.Add(new Album { AlbumTitle = "Album 1", AlbumCover = "cover1.jpg" });
            Albums.Add(new Album { AlbumTitle = "Album 2", AlbumCover = "cover2.jpg" });
            Albums.Add(new Album { AlbumTitle = "Album 3", AlbumCover = "cover3.jpg" });
            Albums.Add(new Album { AlbumTitle = "Album 4", AlbumCover = "cover4.jpg" });
            ShowAlbum = new Command(async () =>
            {
                await Shell.Current.GoToAsync("UserDetailsPage");
                Albums = new ObservableCollection<Album>(currentusersalbums);

            });

        }
    }
    }
