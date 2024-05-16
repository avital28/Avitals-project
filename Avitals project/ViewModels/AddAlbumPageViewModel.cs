﻿using Avitals_project.Models;
using Avitals_project.Services;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Devices.Sensors;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Avitals_project.ViewModels
{
    public class AddAlbumPageViewModel : MediaMethodsViewModel
    {
        #region Private fields
        private bool isfound;
        private bool isvisible;
        private bool isdoneloading;
        private string location;
        private String longtitude;
        private String latitude;
        private string title;
        private const string notfoundmessage = "No albums were found nearby";
        private const string loadingmessage = "Searching for albums in your area";
        private const string doneloadingmessage = "Albums in your area";
        private string headermessage;
        private string albumcover;
        #endregion
        #region Properties
        public ICommand JoinAlbum { get; set; }
        public ICommand LoadAlbums { get; set; }
        public ICommand Create { get; set; }
        public ICommand CreateAlbum {  get; set; }
        public Album album { get; set; }
        public string Title { get => title; set { if (title != value) { title = value; OnPropertyChange(); } } }
        public bool IsVisible { get { return isvisible; } set { if (isvisible != value) { isvisible = value; OnPropertyChange(); } } }
        public string Location_ { get { return location; } set { if (location != value) { location = value; OnPropertyChange(); } }}
        public bool IsFound { get { return isfound; } set { if (isfound != value) { isfound = value; OnPropertyChange(); } } }
        public bool IsDoneLoading { get { return isdoneloading; } set { if (isdoneloading != value) { isdoneloading = value; OnPropertyChange(); } } }
        public string HeaderMessage { get { return headermessage; } set { if (headermessage != value) { headermessage = value; OnPropertyChange(); } } }
        public string AlbumCover { get { return albumcover; } set { if (albumcover != value) { albumcover = value; OnPropertyChange(); } } }

        public string NotFoundMessage { get { return notfoundmessage; } }
        public ObservableCollection<Album> Albums { get; set; }
        public Dictionary<string, object> nav = new Dictionary<string, object>();
        #endregion
        #region Methods
        //public Animation CreateAnimation ()
        //{
        //    var animation = new Animation ();   
        //    animation.
        //}
        private async void JoinAlbum1 (Album al)
        {
            nav.Clear();
            al.Memebers.Add(new User() { Firstname = "A" });
            al.Memebers.Add(new User() { Firstname = "B" });
            al.Memebers.Add(new User() { Firstname = "C" });
            al.Memebers.Add(new User() { Firstname = "D" });
            al.Memebers.Add(new User() { Firstname = "E" });

            al.Media.Add(new Media("cover2.jpg"));
            al.Media.Add(new Media("cover3.jpg"));
            al.Media.Add(new Media("https://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4"));

            IsVisible = false;
            nav.Add("album", al);
            await Shell.Current.GoToAsync("ViewAlbumDetailsPage", nav);
        }
        private async void GetLocation()
        {
            Location l = await Geolocation.GetLocationAsync();
            GetGeocodeReverseData(l.Longitude, l.Latitude); 
        }
        private async void GetGeocodeReverseData(double longitude, double latitude)
        {
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                IEnumerable<Placemark> placemarks = await Geocoding.Default.GetPlacemarksAsync(latitude, longitude);

                Placemark placemark = placemarks?.FirstOrDefault();

                if (placemark != null)
                {
                    Location_ = placemark.Thoroughfare.ToString();
                }
            });
        }
                #endregion
        public AddAlbumPageViewModel(UserService service): base (service)
        {
            //GetLocation();
            IsVisible = false;
            IsFound = false;
            IsDoneLoading = false;
            IsOpen= false;
            Title = "Album title";
            Cover = "emptyalbumcover.jpg";
            HeaderMessage = loadingmessage;
            Albums = new ObservableCollection<Album>();
            Albums.Add(new Album { AlbumTitle = "Album 1", AlbumCover = "cover1.jpg" });
            Albums.Add(new Album { AlbumTitle = "Album 2", AlbumCover = "cover2.jpg" });
            Albums.Add(new Album { AlbumTitle = "Album 3", AlbumCover = "cover3.jpg" });
            Albums.Add(new Album { AlbumTitle = "Album 4", AlbumCover = "cover4.jpg" });
            Albums.Add(new Album { AlbumTitle = "Album 4", AlbumCover = "cover4.jpg" });
            Albums.Add(new Album { AlbumTitle = "Album 5", AlbumCover = "cover4.jpg" });
            Albums.Add(new Album { AlbumTitle = "Album 6", AlbumCover = "cover3.jpg" });
            
            LoadAlbums = new Command(async () =>  
            {
                try
                {
                    Location l = await Geolocation.GetLocationAsync();
                    longtitude = l.Longitude.ToString();
                    latitude = l.Latitude.ToString();

                    ObservableCollection<Album> a = new ObservableCollection<Album>(await service.GetAlbumsByLocation(longtitude, latitude));

                    IsDoneLoading = true;
                    HeaderMessage = doneloadingmessage;
                    if (a != null)
                    {
                        IsFound = true;
                        foreach(var item in a)
                        {
                            Albums.Add(item);
                        }
                        await Shell.Current.DisplayAlert("Albums were found", "", "אישור");

                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("There has been an error", "", "אישור");

                    }

                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }




               

            });
            JoinAlbum = new Command<Album>(JoinAlbum1);
            CreateAlbum = new Command(async () =>
            {
                try
                {
                    Location l = await Geolocation.GetLocationAsync();
                    longtitude = l.Longitude.ToString();
                    latitude = l.Latitude.ToString();
                    Album a = new Album() { AlbumCover = Cover, AlbumTitle = Title, Latitude = latitude, Longitude = longtitude };
                    FileResult f = currentfile;
                    var response = await service.CreateAlbumAsync(a, f);
                    if (response == true)
                    {
                        DisplayAlbumsPageViewModel.AllUserssAlbums.Add(a);
                        DisplayAlbumsPageViewModel.CurrentAdminsAlbums.Add(a);
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

            Create = new Command(async () =>
                {
                    IsVisible = true;  

                });


        }
        
    }
}

