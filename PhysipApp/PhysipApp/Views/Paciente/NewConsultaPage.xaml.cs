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
	public partial class NewConsultaPage : ContentPage
	{
		public NewConsultaPage()
		{
			InitializeComponent();
			BindingContext = new NewConsultaViewModel();
		}
		protected override void OnDisappearing()
		{
			base.OnDisappearing();
		}
	}
}