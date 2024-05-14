using Avitals_project.ViewModels;

namespace Avitals_project.Views;

public partial class AdminPage : ContentPage
{
	public AdminPage(AdminPageViewModel vm)
	{
		this.BindingContext = vm;	
		InitializeComponent();
	}
}