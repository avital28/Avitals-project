using Avitals_project.ViewModels;

namespace Avitals_project.Views;

public partial class AlbumPage : ContentPage
{
	public AlbumPage(AlbumPageViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}