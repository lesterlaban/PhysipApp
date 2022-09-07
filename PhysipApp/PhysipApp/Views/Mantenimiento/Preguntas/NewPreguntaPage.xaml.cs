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
	public partial class NewPreguntaPage : ContentPage
	{
		public NewPreguntaPage()
		{
			InitializeComponent();
			BindingContext = new NewPreguntaViewModel();
		}
		NewPreguntaViewModel ViewModel { get => BindingContext as NewPreguntaViewModel; set => BindingContext = value; }
		private void Picker_SelectedIndexChanged(object sender, EventArgs e)
		{
			var selectedOption = (sender as Picker).SelectedItem;
			ViewModel.ChangeEncuestaCommand.Execute(selectedOption);
		}
	}
}