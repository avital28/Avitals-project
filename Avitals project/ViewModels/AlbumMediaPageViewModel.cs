using Avitals_project.Models;
using Avitals_project.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avitals_project.ViewModels
{
    [QueryProperty(nameof(Album), "album")]
    public class AlbumMediaPageViewModel:ViewModel
    {
        #region private fields
        private Album album;
        private bool isvisiblepic;
        private bool isvisiblevideo;
        private static int index = 0;
#endregion
        #region public properties
        public Album Album { get { return album; } set { if (album != value) { album = value; OnPropertyChange(); } } }
        public bool IsVisiblePic { get { IsVisible1(); return isvisiblepic; } set { if (isvisiblepic != value) { isvisiblepic = value; OnPropertyChange(); } } }
        public bool IsVisibleVideo { get { IsVisible1(); return isvisiblevideo; } set { if (isvisiblevideo != value) { isvisiblepic = value; OnPropertyChange(); } } }

        #endregion
        #region Methods
        private void IsVisible1()
        {
            if (index%2==0)
            {
                IsVisiblePic= true;
                IsVisibleVideo = false;

            }
            else
            {
                IsVisiblePic = false;
                IsVisibleVideo = true;
            }
            index++;
            //check using index
            // if it's a picture - isvisiblepic=true, isvisiblevideo=false
        }
        #endregion
        public AlbumMediaPageViewModel(UserService service)
        {
            //if (album != null)
            //{
            //    Images = new ObservableCollection<string>(album.Media);
            //}
            //else
            //{ Images = new ObservableCollection<string>(); }
        }
        #region Capturing media methods
        public async void TakePhoto()
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

                if (photo != null)
                {
                    
                    string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                    using Stream sourceStream = await photo.OpenReadAsync();
                    using FileStream localFileStream = File.OpenWrite(localFilePath);

                    await sourceStream.CopyToAsync(localFileStream);
                    //Add the captured photo to the observable collection 
                }
            }
            
        }
        public async void TakeVideo()
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult photo = await MediaPicker.Default.CaptureVideoAsync();

                if (photo != null)
                {

                    string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                    using Stream sourceStream = await photo.OpenReadAsync();
                    using FileStream localFileStream = File.OpenWrite(localFilePath);

                    await sourceStream.CopyToAsync(localFileStream);
                    //Add the captured video to the observable collection 
                }
            }

        }
        #endregion

    }
}
