using PhysipApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhysipApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewUsuarioPage : ContentPage
	{
		public NewUsuarioPage()
		{
			InitializeComponent();
			this.BindingContext = new NewUsuarioViewModel();
		}
		protected override void OnDisappearing()
		{
			base.OnDisappearing();
		}
	}
}