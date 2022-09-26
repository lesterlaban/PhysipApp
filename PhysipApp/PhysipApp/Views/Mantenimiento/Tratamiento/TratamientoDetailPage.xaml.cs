using PhysipApp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace PhysipApp.Views
{
	public partial class TratamientoDetailPage : ContentPage
	{
		public TratamientoDetailPage()
		{
			InitializeComponent();
			BindingContext = new TratamientoDetailViewModel();
		}
	}
}