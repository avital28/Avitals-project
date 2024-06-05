using Avitals_project.ViewModels;

namespace Avitals_project.Views;

public partial class AlbumDataPage : ContentPage
{
	public AlbumDataPage(AlbumDataPage vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}