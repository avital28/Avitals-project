using Avitals_project.ViewModels;

namespace Avitals_project.Views;

public partial class ShowAllAlbums : ContentPage
{
	public ShowAllAlbums(ShowAllAlbumsViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}