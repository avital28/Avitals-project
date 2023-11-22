using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Avitals_project.ViewModels
{
    public class WelcomePageViewModel:ViewModel
    {
        public ICommand ToLogin;
        public ICommand ToRegister;

        public WelcomePageViewModel() 
        {
            ToLogin = new Command(async () => { await AppShell.Current.GoToAsync("Login"); }) ;
        }
    }
}
