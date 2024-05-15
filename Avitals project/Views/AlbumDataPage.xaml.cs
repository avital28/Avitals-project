using Avitals_project.ViewModels;

namespace Avitals_project.Views;

public partial class AlbumDataPage : ContentPage
{
	public AlbumDataPage(AdminPageViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}