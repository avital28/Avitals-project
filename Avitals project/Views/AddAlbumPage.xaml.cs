using Avitals_project.ViewModels;

namespace Avitals_project.Views;

public partial class AddAlbumPage : ContentPage
{
	public AddAlbumPage( AddAlbumPageViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
	protected override async void OnAppearing()
	{
		base.OnAppearing();
        var vm = this.BindingContext as AddAlbumPageViewModel;
        if (vm != null)
        {
            vm.LoadAlbums();
        }



    }

	
}