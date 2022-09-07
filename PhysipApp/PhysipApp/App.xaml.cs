using PhysipApp.Services;
using PhysipApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhysipApp
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
			DependencyService.Register<ApiService>();
			MainPage = new LoginPage();
		}

		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}

