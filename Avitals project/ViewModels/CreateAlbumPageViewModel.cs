using System;
using System.Collections.Generic;
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
        public ICommand CreateAlbum { get; set; }
        public ICommand Decline { get; set; }


        #endregion

        public CreateAlbumPageViewModel() 
        {
            Cover = "emptyalbumcover.jpg";
            Title = "Album title";
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
        }   
    }

}


