using Avitals_project.ViewModels;

namespace Avitals_project.Views;

public partial class LoadingPage : ContentPage
{
	public LoadingPage(LoadingPageViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}