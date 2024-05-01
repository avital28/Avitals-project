using Avitals_project.ViewModels;

namespace Avitals_project.Views;

public partial class CreateAlbumPage : ContentPage
{
	public CreateAlbumPage(CreateAlbumPageViewModel vm)
	{
        
        this.BindingContext = vm;
		InitializeComponent();
	}
}