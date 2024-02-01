﻿using Avitals_project.Services;
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
            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

                if (photo != null)
                {

                    string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                    using Stream sourceStream = await photo.OpenReadAsync();
                    using FileStream localFileStream = File.OpenWrite(localFilePath);

                    await sourceStream.CopyToAsync(localFileStream);
                    Cover = localFilePath;
                }
            }
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
                     Album a= new Album() {AlbumCover=Cover, AlbumTitle=Title, IsPublic=true };
                    await service.CreateAlbumAsync(a);
                }
                catch (Exception e)
                {

                }
            });
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


