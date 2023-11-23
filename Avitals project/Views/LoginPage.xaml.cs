using Avitals_project.ViewModels;

namespace Avitals_project.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageViewModel lp)
	{
        this.BindingContext = lp;
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        //if(((AppShell)AppShell.Current).user != null)
        //{
        //    Shell.Current.GoToAsync()
        //}
        base.OnAppearing();
    }
}
