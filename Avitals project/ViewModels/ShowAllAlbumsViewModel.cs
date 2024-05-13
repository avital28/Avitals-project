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
    [QueryProperty(nameof(List<Album>), "Albums")]
    public class ShowAllAlbumsViewModel:ViewModel
    {
        #region private fields
        private int mediacount;
        public ObservableCollection<Album> Albums;
        #endregion
        #region Properties
        public string AlbumCover { get; set; }
        public ICommand ShowAlbum { get; set; }
        public ICommand AddAlbum { get; set; }
        public Dictionary<string, object> nav = new Dictionary<string, object>();
        public int MediaCount { get { return mediacount; } }
        #endregion
        #region Methods
        


        private async void ShowAlbum1(Album al)
        {
            al.Media.Add(new Media("cover2.jpg"));
            al.Media.Add(new Media("cover3.jpg"));
            al.Media.Add(new Media("https://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4"));
            foreach (var item in al.Media)
            {
                AlbumMediaPageViewModel.Media.Add(item);
            }
            //al.Media.Add("cover2.jpg");
            //al.Media.Add("cover3.jpg");
            //al.Media.Add("https://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4");
            nav.Clear();
            nav.Add("album", al);
            await Shell.Current.GoToAsync("AlbumMediaPage", nav);
            //await Shell.Current.GoToAsync("Upload");
        }
        #endregion
        public ShowAllAlbumsViewModel(UserService service)
        {
            Albums = new ObservableCollection<Album>();

            ShowAlbum = new Command<Album>(ShowAlbum1);
            AddAlbum = new Command(async =>
            {


            });

        }
    }
}
