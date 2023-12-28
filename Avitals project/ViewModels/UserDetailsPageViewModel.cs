using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avitals_project.Models;
using System.Text.Json;
namespace Avitals_project.ViewModels
{
    public class UserDetailsPageViewModel:ViewModel
    {

        #region private fields
        private string firstname;
        private string lastname;
        private string username;
        private string password;
        #endregion
      
        public UserDetailsPageViewModel()
        {
            //if (user != null)
            //{
            //    //default values
            //}
        }
    }
}
