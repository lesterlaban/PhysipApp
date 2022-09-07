using PhysipApp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace PhysipApp.Views
{
	public partial class PreguntaDetailPage : ContentPage
	{
		public PreguntaDetailPage()
		{
			InitializeComponent();
			BindingContext = new PreguntaDetailViewModel();
		}
	}
}