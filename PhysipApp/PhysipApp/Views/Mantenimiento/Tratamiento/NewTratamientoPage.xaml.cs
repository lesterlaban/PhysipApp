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
	public partial class NewTratamientoPage : ContentPage
	{
		public NewTratamientoPage()
		{
			InitializeComponent();
			BindingContext = new NewTratamientoViewModel();
			CollectionView collectionView = new CollectionView
			{
				SelectionMode = SelectionMode.Multiple
			};
		}

	}
}