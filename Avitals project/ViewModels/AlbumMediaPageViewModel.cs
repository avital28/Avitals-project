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
    public class AlbumMediaPageViewModel:ViewModel
    {
        #region private fields
        private Album album;
        private Media current;
        private static FileResult currentfile = null;
        private string cover;
        private bool isopen;
        #endregion
        #region public properties
        public Album Album { get { return album; } set { if (album != value) { album = value; OnPropertyChange(); } } }
        public Media Current { get { return current; } set { if (current != value) { current = value; OnPropertyChange(); } } }
        public ICommand AddMedia { get; set; }
        public ICommand TakePhoto { get; set; }
        public ICommand TakeVideo { get; set; }
        public ICommand ChooseFromGallery { get; set; }
        public ICommand LoadMedia { get; set; }
        public ICommand ChangePhoto { get; set; }
        public ICommand Tapped { get; set; }
        public string Cover { get => cover; set { if (cover != value) { cover = value; OnPropertyChange(); } } }
        public bool IsOpen { get => isopen; set { if (isopen != value) { isopen = value; OnPropertyChange(); } } }


        public static ObservableCollection<Media> Media { get; set; } = new ObservableCollection<Media>();
        public  ObservableCollection<Media> Displayed { get; set; } = new ObservableCollection<Media>();

        #endregion
        #region Methods
        public async void CapturePhoto()
        {
            FileResult photo = new FileResult("a");
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                if (MediaPicker.Default.IsCaptureSupported)
                {

                    photo = await MediaPicker.Default.CapturePhotoAsync();

                    if (photo.FullPath != "")
                    {

                        string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                        using Stream sourceStream = await photo.OpenReadAsync();
                        using FileStream localFileStream = File.OpenWrite(localFilePath);

                        await sourceStream.CopyToAsync(localFileStream);
                        Current = new Media(localFilePath);
                        currentfile = photo;
                        Cover = localFilePath;
                        
                       
                    }
                }


            }
         );
        }
        public async void CaptureVideo()
        {
            
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                if (MediaPicker.Default.IsCaptureSupported)
                {
                    FileResult video = await MediaPicker.Default.CaptureVideoAsync();

                    if (video != null)
                    {

                        string localFilePath = Path.Combine(FileSystem.CacheDirectory, video.FileName);

                        using Stream sourceStream = await video.OpenReadAsync();
                        using FileStream localFileStream = File.OpenWrite(localFilePath);

                        await sourceStream.CopyToAsync(localFileStream);
                        Current = new Media(localFilePath);
                        currentfile=video;
                        Cover = localFilePath;
                        IsOpen = false;

                    }
                }
            });
        }
        private async void ChooseFromGallery1()
        {
            FileResult photo = new FileResult("a");
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                if (MediaPicker.Default.IsCaptureSupported)
                {

                    photo = await MediaPicker.Default.PickPhotoAsync();
                    if (photo != null)
                    {
                        string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                        using Stream sourceStream = await photo.OpenReadAsync();
                        using FileStream localFileStream = File.OpenWrite(localFilePath);

                        await sourceStream.CopyToAsync(localFileStream);
                        Current = new Media(localFilePath);
                        IsOpen = false;

                    }
                }
            });
        }
        #endregion
        public AlbumMediaPageViewModel(UserService service)
        {
            IsOpen = false;
            Cover = "photoalbumicon.png";
            AddMedia = new Command(async () =>
            {
                try
                {

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
            if (Media != null)
            {
                Displayed = new ObservableCollection<Media>();
                foreach (var item in Media)
                {
                    Displayed.Add(item);
                }
            }
           
            TakePhoto = new Command(CapturePhoto);
            TakeVideo= new Command(CaptureVideo);   
            ChooseFromGallery = new Command(ChooseFromGallery1);
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
