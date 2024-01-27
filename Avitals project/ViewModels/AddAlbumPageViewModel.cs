using Avitals_project.Models;
using Avitals_project.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Avitals_project.ViewModels
{
    public class AddAlbumPageViewModel:ViewModel
    {
        #region Private fields
        private bool isfound;
        private bool isdoneloading;
        private String longtitude;
        private String latitude;
        private const string notfoundmessage = "No albums were found nearby";
        private const string loadingmessage = "Searching for albums in your area";
        private const string doneloadingmessage = "Albums in your area";
        private string headermessage;
        #endregion
        #region Properties
        public ICommand JoinAlbum {  get; set; }    
        public ICommand LoadAlbums { get; set;}
        public ICommand CreateAlbum { get; set; }

        public bool IsFound { get { return isfound; } set { if (isfound != value) { isfound = value; OnPropertyChange(); } } }
        public bool IsDoneLoading { get { return isdoneloading; } set { if (isdoneloading != value) { isdoneloading = value; OnPropertyChange(); } } }
        public string HeaderMessage { get { return headermessage; } set { if (headermessage != value) { headermessage = value; OnPropertyChange(); } } }
        public string NotFoundMessage { get { return notfoundmessage; } }
        public ObservableCollection<Album> Albums { get; set; }
        #endregion
        #region Methods
        
        #endregion
        public AddAlbumPageViewModel(UserService service) 
        {
            //
            IsDoneLoading = false;
            HeaderMessage = loadingmessage;
            LoadAlbums = new Command(async () =>
            {
                try
                {
                    Location l = Geolocation.GetLocationAsync().Result;
                    longtitude = l.Longitude.ToString();
                    latitude = l.Latitude.ToString();
                    Albums = new ObservableCollection<Album>();
                    Albums.Add(new Album { AlbumTitle = "Album1", AlbumCover = "usericon.png" });
                    Album a = Albums[0];
                    //Albums = new ObservableCollection<Album>( /*service.GetAlbumsByLocation(longtitude, latitude).Result*/);
                    IsDoneLoading = true;
                    HeaderMessage = doneloadingmessage;
                    if (Albums != null)
                    {
                        IsFound = true;


                    }
                    else
                    {
                        IsFound = false;

                    }

                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            });

            CreateAlbum = new Command(async () =>
            {


            });
            //JoinAlbum= new Command (async()=>
            //{
            //    try 
            //})


        }


        }
    }

