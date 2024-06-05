using Avitals_project.Models;
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
 
        private async void JoinAlbum1 (Album al)
        {
            nav.Clear();
            var a = await userService.GetMediaByAlbumAsync(al); 
            IsVisible = false;
            nav.Add("album", al);
            await Shell.Current.GoToAsync("ViewAlbumDetailsPage", nav);
        }
        public async Task GetLocation()
        {
            Location l = await Geolocation.GetLocationAsync();
            try
            {
                await MainThread.InvokeOnMainThreadAsync(async () =>
                await GetGeocodeReverseData(l.Longitude, l.Latitude));
            }
            catch(Exception ex) { }
        }
        private async Task GetGeocodeReverseData(double longitude, double latitude)
        {
            try
            {
              //  await MainThread.InvokeOnMainThreadAsync(async () =>
                //{
                    IEnumerable<Placemark> placemarks = await Geocoding.Default.GetPlacemarksAsync(latitude, longitude);

                    Placemark placemark = placemarks?.FirstOrDefault();

                    if (placemark != null)
                    {
                        Location_ = placemark.Thoroughfare.ToString();
                    }
                //});
            }
            catch(Exception ex) { }   
        }

        public async Task LoadAlbums()
        {
            Albums.Clear();
            try
            {
                Location l = await Geolocation.GetLocationAsync();
                longtitude = l.Longitude.ToString();
                latitude = l.Latitude.ToString();
                List<Album> albums = await this.userService.GetAlbumsByLocation(longtitude, latitude);
                
                if (albums != null)
                {
                    
                    foreach (var item in albums)
                    {
                        Albums.Add(item);
                    }
                    
                    IsFound = true;
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
        }
                #endregion
        public AddAlbumPageViewModel(UserService service): base (service)
        {
            
            IsVisible = false;
            IsFound = false;
            IsDoneLoading = false;
            IsOpen= false;
            Title = "Album title";
            Cover = "emptyalbumcover.jpg";
            HeaderMessage = loadingmessage;
            Albums = new ObservableCollection<Album>();
            
         
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

