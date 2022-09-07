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
	public partial class NewRecursoPage : ContentPage
	{
		public NewRecursoPage()
		{
			InitializeComponent();
			BindingContext = new NewRecursoViewModel();
		}

	}
}