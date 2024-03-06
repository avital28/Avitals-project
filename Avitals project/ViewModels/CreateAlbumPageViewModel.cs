using Avitals_project.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Avitals_project.Models;
namespace Avitals_project.ViewModels
{
    public class CreateAlbumPageViewModel:ViewModel
    {
        #region Private fields
        private string title;
        private string cover;
        private bool isopen;
        private string longtitude;
        private string latitude;
        private static FileResult currentfile=null;
        #endregion
        #region Properties
        public string Title  { get => title; set { if (title != value) { title = value; OnPropertyChange(); } } }
        public string Cover { get => cover; set { if (cover != value) { cover = value; OnPropertyChange(); } } }
        public bool IsOpen { get => isopen; set { if (isopen != value) { isopen = value; OnPropertyChange(); } } }
        public ObservableCollection<string> DropDownData { get; set; }
        public ICommand CreateAlbum { get; set; }
        public ICommand Decline { get; set; }
        public ICommand ChangePhoto { get; set; }   
        public ICommand TakePhoto { get; set; }

        #endregion
        #region Methods
        public async void CapturePhoto()
        {
            FileResult photo = new FileResult("");
           await MainThread.InvokeOnMainThreadAsync(async () =>
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {

                 photo = await MediaPicker.Default.CapturePhotoAsync();

                if (photo != null)
                {

                    string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                    using Stream sourceStream = await photo.OpenReadAsync();
                    using FileStream localFileStream = File.OpenWrite(localFilePath);

                    await sourceStream.CopyToAsync(localFileStream);
                    Cover = localFilePath;
                    IsOpen = false;
                    currentfile = photo;
                }
            }
            
            
        }
        );
            
        }
            #endregion
            public CreateAlbumPageViewModel(UserService service) 
            {
            Cover = "emptyalbumcover.jpg";
            IsOpen = false;
            DropDownData = new ObservableCollection<string>() { "Take a picture", "Choose from gallery" };
            CreateAlbum = new Command(async () =>
            {
                try
                {
                    Location l = await Geolocation.GetLocationAsync();
                    longtitude = l.Longitude.ToString();
                    latitude = l.Latitude.ToString();
                    Album a = new Album() {AlbumCover=Cover, AlbumTitle=Title, IsPublic=true, Latitude=latitude, Longitude=longtitude };
                     var response=await service.CreateAlbumAsync(a, currentfile);
                    if (response==true)
                    {
                        DisplayAlbumsPageViewModel.currentusersalbums.Add(a);
                        await Shell.Current.DisplayAlert("Album created successfuly", "", "אישור");

                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Failed", "", "אישור");

                    }
                }
                catch (Exception e)
                {

                }
            });
            TakePhoto = new Command(CapturePhoto);
            Decline = new Command(async () =>
            {
                try
                {
                    //await Shell.Current.GoToAsync("");
                }
                catch (Exception e)
                { }
            });
            ChangePhoto = new Command(async () =>
            {
                IsOpen = true;
            });
        }   
    }

}


