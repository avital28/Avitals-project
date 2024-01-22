using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avitals_project.ViewModels
{
    public class LoadingPageViewModel:ViewModel
    {
        private bool _isBusy;
        public bool IsBusy { get => _isBusy; set { _isBusy = value; OnPropertyChange(); } }
    }
}
