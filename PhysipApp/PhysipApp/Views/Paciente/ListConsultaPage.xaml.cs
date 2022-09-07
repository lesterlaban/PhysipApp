using PhysipApp.ViewModels;
using Xamarin.Forms;

namespace PhysipApp.Views
{
	public partial class ListConsultaPage : ContentPage
	{
		ListConsultaViewModel _viewModel;
		public ListConsultaPage()
		{
			InitializeComponent();
			BindingContext = _viewModel = new ListConsultaViewModel();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			_viewModel.OnAppearing();
		}
		protected override void OnDisappearing()
		{
			base.OnDisappearing();
		}
	}
}