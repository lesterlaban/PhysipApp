using PhysipApp.Models;
using PhysipApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhysipApp.Views
{
	public partial class NewNivelPage : ContentPage
	{
		public NewNivelPage()
		{
			InitializeComponent();
			BindingContext = new NewNivelViewModel();
		}
	}
}