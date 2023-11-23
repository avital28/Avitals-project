using Avitals_project.ViewModels;

namespace Avitals_project.Views;

public partial class WelcomePage : ContentPage
{
	public WelcomePage(WelcomePageViewModel wp)
	{
		this.BindingContext = wp;
		InitializeComponent();
	}
}