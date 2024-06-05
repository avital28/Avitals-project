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
    public class AlbumMediaPageViewModel:MediaMethodsViewModel
    {
        #region private fields
        private Album album;
        private Media current;
        private string cover;
        private bool isopen;
        #endregion
        #region public properties
        public Album Album { get { return album; } set { if (album != value) { album = value; UpdateMedia(); OnPropertyChange(); } } }
        public Media Current { get { return current; } set { if (current != value) { current = value; OnPropertyChange(); } } }
        public ICommand AddMedia { get; set; }
       
        public ICommand LoadMedia { get; set; }
    
        public ICommand Tapped { get; set; }

       

        public ObservableCollection<Media> Displayed { get; set; } = new ObservableCollection<Media>();

        #endregion
        #region Methods
        private async void UpdateMedia()
        {
            Displayed.Clear();
            if (Album.MediaCount>0)
            {
                foreach (var item in Album.Media)
                {
                    Displayed.Add(item);
                }
            }
        }
        
        #endregion
        public AlbumMediaPageViewModel(UserService service): base (service)
        {
            IsOpen = false;
            Cover = "photoalbumicon.png";
            AddMedia = new Command(async () =>
            {
                try
                {
                    Current = new Media(localFilePath);
                    var response = await service.UploadMedia(Album, Current, currentfile);
                    if (response!=null)
                    {
                        Displayed.Add(new Media(response));
                    }
                    

                    
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }

            });
            Tapped = new Command(async () =>
            {
                IsOpen = true;
            });
           
           
           
            ChangePhoto = new Command(async () =>
            {
                IsOpen = true;
            });
            //if (album != null)
            //{
            //    Images = new ObservableCollection<string>(album.Media);
            //}
            //else
            //{ Images = new ObservableCollection<string>(); }
        }
        

    }
}
