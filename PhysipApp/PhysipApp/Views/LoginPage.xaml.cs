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
	public partial class LoginPage : ContentPage
	{
		public LoginPage()
		{
			InitializeComponent();
			Routing.RegisterRoute(nameof(NewUsuarioPage), typeof(NewUsuarioPage));
			Routing.RegisterRoute(nameof(AppShell), typeof(AppShell));
			this.BindingContext = new LoginViewModel();
		}
		protected override void OnDisappearing()
		{
			base.OnDisappearing();
		}
	}
}