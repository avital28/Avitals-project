using Avitals_project.ViewModels;

namespace Avitals_project.Views;

public partial class AddAlbumPage : ContentPage
{
	public AddAlbumPage( AddAlbumPageViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
    //protected override async void OnAppearing()
    //{
    //    base.OnAppearing();
	   // popup_p.Show();
		
	
		
    //}

    private void Button_Clicked(object sender, EventArgs e)
    {
		bool x= true;
    }
}