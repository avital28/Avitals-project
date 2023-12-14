using Avitals_project.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Avitals_project.Controls
{
    public class PasswordViewModel:ViewModel
    {
        private bool isvisible;
        public bool IsVisible { get { return isvisible; } set { if (isvisible != value) { isvisible = value; OnPropertyChange(); } } }
        public ICommand ShowPassCommand { get; set; }   

        public PasswordViewModel ()
        {
            isvisible = true;
            ShowPassCommand = new Command(c => isvisible = !isvisible);
        }
    }
}
