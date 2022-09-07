using PhysipApp.Models;
using System;
using Xamarin.Forms;

namespace PhysipApp.ViewModels
{
	public class NewRecursoViewModel : BaseViewModel
	{
		public NewRecursoViewModel()
		{
			Title = "Registro de Recursos";
			SaveCommand = new Command(OnSave, ValidateSave);
			CancelCommand = new Command(OnCancel);
			this.PropertyChanged +=
			(_, __) => SaveCommand.ChangeCanExecute();
		}


		private string _titulo;
		public string Titulo
		{
			get => _titulo;
			set
			{
				SetProperty(ref _titulo, value);
			}
		}

		private string _descripcion;
		public string Descripcion
		{
			get => _descripcion;
			set
			{
				SetProperty(ref _descripcion, value);
			}
		}

		private string _url;
		public string Url
		{
			get => _url;
			set
			{
				SetProperty(ref _url, value);
			}
		}

		private bool ValidateSave()
		{
			return !string.IsNullOrEmpty(Titulo) 
				&& !string.IsNullOrEmpty(Descripcion) && !string.IsNullOrEmpty(Url);
		}

		public Command CancelCommand { get; }
		private async void OnCancel()
		{
			// This will pop the current page off the navigation stack
			await Shell.Current.GoToAsync("..");
		}

		public Command SaveCommand { get; }
		private async void OnSave()
		{
			IsBusy = true;
			var newItem = Recurso.New(Titulo, Descripcion, Url);
			var result = await _servicioApi.Add("Recursos", newItem);
			IsBusy = false;
			if (result)
				await Shell.Current.GoToAsync("..");
		}

		

	}
}
