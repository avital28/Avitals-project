using Avitals_project.ViewModels;

namespace Avitals_project.Views;

public partial class NewPage1 : ContentPage
{
	public NewPage1(AlbumMediaPageViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}