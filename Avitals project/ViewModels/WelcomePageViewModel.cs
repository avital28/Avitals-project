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
        public ICommand ToLogin { get; set; }   
        public ICommand ToRegister { get; set; }

        public WelcomePageViewModel() 
        {
            ToLogin = new Command(async () => { await AppShell.Current.GoToAsync("LoginPage"); }) ;
            ToRegister = new Command(async () => { await AppShell.Current.GoToAsync("RegisterPage"); });
        }
    }
}
