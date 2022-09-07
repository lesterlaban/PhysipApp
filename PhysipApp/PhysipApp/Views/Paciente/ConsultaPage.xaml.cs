using PhysipApp.ViewModels;
using Xamarin.Forms;

namespace PhysipApp.Views
{
	public partial class ConsultaPage : ContentPage
	{
		public ConsultaPage()
		{
			InitializeComponent();
			BindingContext = new ConsultaViewModel();
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
		}
	}
}