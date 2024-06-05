using Avitals_project.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avitals_project.ViewModels
{
    //[QueryProperty(nameof(Users), "users")]
    public class AlbumDataPageViewModel: ViewModel
    {
        private List<User> users;   
        public List<User> Users { get { return users; } set { if (users != value) { users = value; UpdateCollection(); OnPropertyChange(); } } }

        public ObservableCollection<User> Members { get; set; } = new ObservableCollection<User>(); 

        private async Task UpdateCollection()
        {
            if (Users.Count>0)
            {
                foreach (var user in Users) 
                { 
                    Members.Add(user);  
                }
            }
        }

    }
}
