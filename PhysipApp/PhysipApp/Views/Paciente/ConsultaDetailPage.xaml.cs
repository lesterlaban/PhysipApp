using PhysipApp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace PhysipApp.Views
{
	public partial class ConsultaDetailPage : ContentPage
	{
		public ConsultaDetailPage()
		{
			InitializeComponent();
			BindingContext = new ConsultaDetailViewModel();
		}
		protected override void OnDisappearing()
		{
			base.OnDisappearing();
		}
	}
}