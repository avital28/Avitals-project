using Avitals_project.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Avitals_project.ViewModels
{
    public class LoginPageViewModel:ViewModel
    {
        private string username;
        private string password;
        public string Username { get => username; set { if (username != value) { username = value; OnPropertyChange(); } } }
        public string Password { get => password; set { if (password != value) { password = value; OnPropertyChange(); } } }

        public ICommand LoginCommand { get; set; }
        public LoginPageViewModel(UserService service)
        {
            LoginCommand=new Command (async ()=>
        }
    }
}
