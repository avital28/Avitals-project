
using Avitals_project.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace Avitals_project.ViewModels
{
    public class MediaMethodsViewModel:ViewModel
    {
       protected UserService userService;
        private string cover;
        private bool isopen;
        protected static FileResult currentfile = null;
        public string Cover { get => cover; set { if (cover != value) { cover = value; OnPropertyChange(); } } }
        public bool IsOpen { get => isopen; set { if (isopen != value) { isopen = value; OnPropertyChange(); } } }
        public ICommand TakePhoto { get; set; }
        public ICommand TakeVideo { get; set; }
        public ICommand ChangePhoto { get; set; }
        public ICommand ChooseFromGallery { get; set; }
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
                        Cover = localFilePath;
                       
                        currentfile = photo;
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
                        
                        currentfile = video;
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
                        Cover = localFilePath;
                        IsOpen = false;
                        currentfile = photo;
                    }
                }
            });
        }
        public MediaMethodsViewModel(UserService service)
        {
            this.userService = service;
            TakePhoto = new Command(CapturePhoto);
            TakeVideo = new Command(CaptureVideo);
            ChooseFromGallery = new Command(ChooseFromGallery1);
            ChangePhoto = new Command(async () =>
            {
                IsOpen = true;
            });
        }
        }
    
}
