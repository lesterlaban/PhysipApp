using PhysipApp.ViewModels;
using Xamarin.Forms;

namespace PhysipApp.Views
{
	public partial class ResultadoEncuestaPage : ContentPage
	{
		ResultadoEncuestaViewModel _viewModel;
		public ResultadoEncuestaPage()
		{
			InitializeComponent();
			BindingContext = _viewModel = new ResultadoEncuestaViewModel();
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			_viewModel.OnAppearing();
		}
	}
}