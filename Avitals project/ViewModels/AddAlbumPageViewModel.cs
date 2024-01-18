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
        private bool isfound;
        
        #endregion
        #region Properties
        public ICommand JoinAlbum {  get; set; }    
        public ICommand CreateAlbum { get; set;}
        public bool IsFound { get { return isfound; } set { if (isfound != value) { isfound = value; OnPropertyChange(); } } }

       

        public ObservableCollection<Album> Albums { get; set; }
        #endregion
        #region Methods

        #endregion
        public AddAlbumPageViewModel(UserService service) 
        {

            CreateAlbum = new Command(async () =>
            {
                try
                {
                    //string longtitude= Geocoding.Default.GetLocationsAsync()
                    Albums = new ObservableCollection<Album>();
                    if (Albums != null)
                    {
                        IsFound = true;

                    }

                    // "Not found" message 
                }
                catch 
                {

                }
            });

            

        }


        }
    }

