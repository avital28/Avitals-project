﻿using Avitals_project.Models;
using Avitals_project.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avitals_project.ViewModels
{
    public class AlbumMediaPageViewModel:ViewModel
    {
       
        #region Properties
        public ObservableCollection<string> Images { get; set; }
        //or
        public Album album;
        #endregion

        public AlbumMediaPageViewModel(UserService service)
        {
            if (album != null)
            {
                Images = new ObservableCollection<string>(album.Media);
            }
            else
            { Images = new ObservableCollection<string>(); }
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
