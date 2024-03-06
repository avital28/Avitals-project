using Avitals_project.ViewModels;

namespace Avitals_project.Views;

public partial class DisplayAlbumsPage : ContentPage
{
	public DisplayAlbumsPage(DisplayAlbumsPageViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
	public DisplayAlbumsPage() { }
	
}