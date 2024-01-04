using Avitals_project.ViewModels;

namespace Avitals_project.Views;

public partial class UserDetailsPage : ContentPage
{
	public UserDetailsPage( UserDetailsPageViewModel vm)
	{
		this.BindingContext = vm;	
		InitializeComponent();
	}
}