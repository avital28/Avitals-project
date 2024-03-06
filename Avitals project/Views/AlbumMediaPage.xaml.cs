using Avitals_project.ViewModels;

namespace Avitals_project.Views;

public partial class AlbumMediaPage : ContentPage
{
	public AlbumMediaPage(AlbumMediaPageViewModel vm)
	{
		this.BindingContext = vm;	
		InitializeComponent();
	}
}