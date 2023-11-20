namespace Avitals_project.Views;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
	}
    protected override void OnAppearing()
    {
        if(((AppShell)AppShell.Current).user != null)
        {
            Shell.Current.GoToAsync()
        }
        base.OnAppearing();
    }
}