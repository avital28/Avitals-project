using Avitals_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avitals_project.ViewModels
{
    [QueryProperty(nameof(Album), "Elephant")]
    public class ViewAlbumDetailsPageViewModel:ViewModel
    {
        private Album album;
        public Album Album { get { return album; } set { if (album != value) { album = value; OnPropertyChange(); } } } 

    }
}
