using PhysipApp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace PhysipApp.Views
{
	public partial class RecursoDetailPage : ContentPage
	{
		public RecursoDetailPage()
		{
			InitializeComponent();
			BindingContext = new RecursoDetailViewModel();
		}
	}
}