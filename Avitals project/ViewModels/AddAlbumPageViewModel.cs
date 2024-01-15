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
    public class AddAlbumPageViewModel:ViewModel
    {
        #region Private fields
        private bool Isfound;
        #endregion
        #region Properties
        public ICommand JoinAlbum {  get; set; }    
        public ICommand AddAlbum { get; set;}
        public bool IsFound { get { return Isfound; } set { if (Isfound != value) { Isfound = value; OnPropertyChange(); } } }


        public ObservableCollection<Album> Albums { get; set; }
        #endregion

        public AddAlbumPageViewModel(UserService service) 
        {
            AddAlbum = new Command(async () =>
            {
                //display existing albums
                 if (Albums!=null)
                {
                    //show existing albums and ask if to add anyway
                }

                 // "Not found" message 
            });


        }

        public async void GetAlbumsByLocation()
        {

        }
    }
}
