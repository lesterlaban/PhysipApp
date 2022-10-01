using PhysipApp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace PhysipApp.Views
{
	public partial class ConsultaRecursoPage : ContentPage
	{
		public ConsultaRecursoPage()
		{
			InitializeComponent();
			BindingContext = new ConsultaRecursoModel();
		}
		protected override void OnDisappearing()
		{
			base.OnDisappearing();
		}
	}
}