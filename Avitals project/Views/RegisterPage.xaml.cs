using Avitals_project.ViewModels;

namespace Avitals_project.Views;

public partial class RegisterPage : ContentPage
{
	public RegisterPage( RegisterPageViewModel rp )
	{
		this.BindingContext = rp;
		InitializeComponent();
	}
}