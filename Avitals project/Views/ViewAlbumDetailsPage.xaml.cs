using Avitals_project.ViewModels;
using CommunityToolkit.Maui.Views;

namespace Avitals_project.Views;

public partial class ViewAlbumDetailsPage : ContentPage
{
	public ViewAlbumDetailsPage(ViewAlbumDetailsPageViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
		ViewAlbumDetailsPage= new NavigationPage(new ViewAlbumDetailsPage());
	}

    
}