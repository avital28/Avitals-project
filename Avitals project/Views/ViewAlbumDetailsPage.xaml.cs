using Avitals_project.ViewModels;
using CommunityToolkit.Maui.Views;

namespace Avitals_project.Views;

public partial class ViewAlbumDetailsPage : ContentPage
{
	public ViewAlbumDetailsPage(ViewAlbumDetailsPageViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	
	}
    //protected override async void OnAppearing()
    //{
    //    base.OnAppearing();
    //    var vm = this.BindingContext as ViewAlbumDetailsPageViewModel;
    //    if (vm != null)
    //    {
    //        vm.LoadMedia();
    //    }



    //}


}