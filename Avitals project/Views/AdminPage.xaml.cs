using Avitals_project.ViewModels;

namespace Avitals_project.Views;

public partial class AdminPage : ContentPage
{
	public AdminPage(AdminPageViewModel vm)
	{
		this.BindingContext = vm;	
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var vm = this.BindingContext as AdminPageViewModel;
        if (vm != null)
        {
            vm.LoadUsers();
        }



    }
}