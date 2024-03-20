using Avitals_project.Models;
using Avitals_project.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avitals_project.ViewModels
{
    [QueryProperty(nameof(Album), "album")]
    public class ViewAlbumDetailsPageViewModel:ViewModel
    {
        #region private fields
        private Album album;
        #endregion
        #region
        public Album Album { get { return album; } set { if (album != value) { album = value; OnPropertyChange(); } } }
        #endregion

        public ViewAlbumDetailsPageViewModel(UserService service)
        {

        }

    }
}
