using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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

        #endregion

        public CreateAlbumPageViewModel() 
        {
            Cover = "emptyalbumcover.jpg";
            IsOpen = false;
            DropDownData = new ObservableCollection<string>() { "Take a picture", "Choose from gallery" };
            CreateAlbum = new Command(async () =>
            {
                try
                {

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


