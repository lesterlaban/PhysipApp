using PhysipApp.Models;
using PhysipApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhysipApp.Views
{
	public partial class NewZonaPage : ContentPage
	{
		public NewZonaPage()
		{
			InitializeComponent();
			BindingContext = new NewZonaViewModel();
		}
	}
}