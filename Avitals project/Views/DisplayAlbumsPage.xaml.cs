using Avitals_project.ViewModels;
using CommunityToolkit.Maui.Views;

namespace Avitals_project.Views;

public partial class DisplayAlbumsPage : ContentPage
{
	public DisplayAlbumsPage(DisplayAlbumsPageViewModel vm)
	{
		this.BindingContext = vm;
		
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var vm = this.BindingContext as DisplayAlbumsPageViewModel;
		if (vm != null)
		{
	     vm.AddToCollection();
		}
		



    }
    public DisplayAlbumsPage() { }
	
}