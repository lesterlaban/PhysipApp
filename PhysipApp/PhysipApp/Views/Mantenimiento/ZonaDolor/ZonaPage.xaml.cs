using PhysipApp.Models;
using PhysipApp.ViewModels;
using PhysipApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhysipApp.Views
{
	public partial class ZonaPage : ContentPage
	{
		ZonaViewModel _viewModel;

		public ZonaPage()
		{
			InitializeComponent();

			BindingContext = _viewModel = new ZonaViewModel();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			_viewModel.OnAppearing();
		}
	}
}