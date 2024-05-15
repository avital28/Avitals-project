using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Avitals_project.Models;
using Avitals_project.Services;


namespace Avitals_project.ViewModels
{
    public class DisplayAlbumsPageViewModel:ViewModel
    {
        #region private fields
        private int mediacount;
        private object selecteditem;
        #endregion
        #region Properties
        public object SelectedItem { get => selecteditem; set { if (selecteditem != value) { selecteditem = value; OnPropertyChange(); } } }
        public string AlbumCover { get; set; }
        public Dictionary<string, object> nav = new Dictionary<string, object>();
        public Dictionary<string, List<object>> nav2 = new Dictionary<string, List<object>>();
        public int MediaCount { get { return mediacount; } }
        public ObservableCollection<string> DropDown { get; set; }
        #region Static lists
        public static List<Album> CurrentAdminsAlbums = new List<Album>();
        public static List<Album> AllUserssAlbums = new List<Album>();
        public static List<Album> CurrentMembersAlbums = new List<Album>();
        #endregion
        #region Observable collections
        public ObservableCollection<Album> Albums { get; set; }
        public ObservableCollection<Album> FilteredAlbums { get; set; }
        public ObservableCollection<Album> AdminAlbums { get; set; }
        public ObservableCollection<Album> MembersAlbums { get; set; }
        #endregion
        #region Commands
        public ICommand ShowAlbum { get; set; }
        public ICommand GetAdminAlbums { get; set; }
        public ICommand AddAlbum { get; set; }
        public ICommand DisplayAllAlbums { get; set; }
        public ICommand DisplayAdmin { get;set; }
        public ICommand DisplaySelectedAlbums { get; set; }

        public ICommand SelectionChnagedCommand { get; set; }
        #endregion
        #endregion
        #region Methods
        public void UpdateLastFour()
        {
            if (AllUserssAlbums.Count >= 1)
            {
                for (int i = AllUserssAlbums.Count - 1; i > AllUserssAlbums.Count - 5 && i >= 0; i--)
                {
                    Albums.Add(AllUserssAlbums[i]);
                }
            }
            if (CurrentAdminsAlbums.Count >= 1)
            {
                for (int i = CurrentAdminsAlbums.Count - 1; i > CurrentAdminsAlbums.Count - 5 && i >= 0; i--)
                {
                    AdminAlbums.Add(CurrentAdminsAlbums[i]);
                }
            }
            if (CurrentMembersAlbums.Count >= 1)
            {
                for (int i = CurrentMembersAlbums.Count - 1; i >= CurrentMembersAlbums.Count - 5 && i >= 0; i--)
                {
                    MembersAlbums.Add(CurrentMembersAlbums[i]);
                }
            }
        }
        public async void AddToCollection()
        {
            Albums.Clear();
            AdminAlbums.Clear();
            MembersAlbums.Clear();
            UpdateLastFour();
        }
        public async void ComboBoxSelectionChanged(object obj)
        {
            var selectionChangedArgs = obj as SelectionChangedEventArgs;
            FilteredAlbums.Clear();
            if (SelectedItem == "My albums")
            {
                if (AdminAlbums != null)
                {
                    foreach (var item in AdminAlbums)
                    {
                        FilteredAlbums.Add(item);
                    }
                }
            }
            else
            {
                if (MembersAlbums != null)
                {
                    foreach (var item in MembersAlbums)
                    {
                        FilteredAlbums.Add(item);
                    }
                }
            }
        }

        private async void ShowAlbum1(Album al)
        {
            List<Media> m = await GetMedia(al);
            if (m!=null)
            {
                foreach (var item in m)
                {
                    al.Media.Add(item);
                    
                }
            }
            //al.Media.Add(new Media("cover2.jpg") );
            //al.Media.Add(new Media("cover3.jpg") );
            //al.Media.Add(new Media("https://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4") );
            //foreach (var item in al.Media)
            //{
            //    AlbumMediaPageViewModel.Media.Add(item);
            //}
            //al.Media.Add("cover2.jpg");
            //al.Media.Add("cover3.jpg");
            //al.Media.Add("https://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4");
            nav.Clear();
            nav.Add("album", al);
            await Shell.Current.GoToAsync("AlbumMediaPage", nav);
            //await Shell.Current.GoToAsync("Upload");
        }
        private async void DisplayAdmin1(Album al)
        {
            if (SelectedItem == "My albums")
            {
                al.Memebers.Add(new User() { Firstname = "Eylon", UserName = "a@gmail.com" });
                al.Memebers.Add(new User() { Firstname = "Avia", UserName = "a@gmail.com" });

                nav.Clear();
                nav.Add("album", al);

                AdminPageViewModel.albumcover = al.AlbumCover;
                await Shell.Current.GoToAsync("AdminPage", nav);
            }
            //await Shell.Current.GoToAsync("Upload");
        }

        private async Task<List<Media>> GetMedia  (Album al)
        {
            UserService s = new UserService();
            return await s.GetMediaByAlbumAsync(al);
        }
        private async void DisplaySelectedAlbums1()
        {
            
            //nav2.Clear();
            //List<object> al = new List<object>();
            //if (AllUserssAlbums.Count != 0)
            //{
            //    for (int i = AllUserssAlbums.Count - 1; i >= 0; i--)
            //    {
            //        al.Add(AllUserssAlbums.ElementAt(i) as object);
            //    }
            //}
            //nav2.Add("Albums", al);


            // Dictionary<string, object> paramaters = new Dictionary<string, object>();
            //paramaters.Add("Albums", nav2); 
            ShowAllAlbumsViewModel.album.Clear();
            if (SelectedItem == "My albums")
            {
                if (CurrentAdminsAlbums != null)
                {
                    foreach (var item in CurrentAdminsAlbums)
                    {
                        ShowAllAlbumsViewModel.album.Add(item);
                    }
                }
            }
            else
            {
                if (CurrentMembersAlbums != null)
                {
                    foreach (var item in CurrentMembersAlbums)
                    {
                        ShowAllAlbumsViewModel.album.Add(item);
                    }
                }
            }
            await Shell.Current.GoToAsync("ShowAllAlbums" );

        }
        private async void DisplayAllAlbums1()
        {
            //nav2.Clear();
            //List<object> al = new List<object>();
            //if (AllUserssAlbums.Count != 0)
            //{
            //    for (int i = AllUserssAlbums.Count - 1; i >= 0; i--)
            //    {
            //        al.Add(AllUserssAlbums.ElementAt(i) as object);
            //    }
            //}
            //nav2.Add("Albums", al);


            // Dictionary<string, object> paramaters = new Dictionary<string, object>();
            //paramaters.Add("Albums", nav2); 
            ShowAllAlbumsViewModel.album.Clear();
            if (AllUserssAlbums.Count != 0)
            {
                for (int i = AllUserssAlbums.Count - 1; i >= 0; i--)
                {
                    ShowAllAlbumsViewModel.album.Add(AllUserssAlbums[i]);
                }
            }
            await Shell.Current.GoToAsync("ShowAllAlbums");

        }
        #endregion
        public DisplayAlbumsPageViewModel(UserService service) 
        {
            Albums = new ObservableCollection<Album>();
            AdminAlbums = new ObservableCollection<Album>();
            MembersAlbums = new ObservableCollection<Album>();
            UpdateLastFour();

            DropDown = new ObservableCollection<string>();
            FilteredAlbums = new ObservableCollection<Album>();
            DropDown.Add("My albums");
            DropDown.Add("Shared with me");
            SelectedItem=DropDown.ElementAt(0);
            if(AdminAlbums!=null)
            {
                foreach(Album album in AdminAlbums)
                {
                    FilteredAlbums.Add(album);
                }
            }
            SelectionChnagedCommand = new Command<object>(ComboBoxSelectionChanged);
            ShowAlbum = new Command<Album>(ShowAlbum1);
            DisplayAllAlbums = new Command(DisplayAllAlbums1);
            DisplaySelectedAlbums = new Command(DisplaySelectedAlbums1);
            DisplayAdmin= new Command<Album>(DisplayAdmin1);    
            AddAlbum = new Command(async =>
            {
               

            });

        }
    }
    }
