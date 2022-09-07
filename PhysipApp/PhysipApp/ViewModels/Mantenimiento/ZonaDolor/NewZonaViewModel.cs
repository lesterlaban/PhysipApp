using PhysipApp.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PhysipApp.ViewModels
{
	public class NewZonaViewModel : BaseViewModel
	{

		private string _descripcionZona;
		public string DescripcionZona
		{
			get => _descripcionZona;
			set
			{
				SetProperty(ref _descripcionZona, value);
			}
		}

		public NewZonaViewModel()
		{
			Title = "Registro de Zonas";
			SaveCommand = new Command(OnSave, ValidateSave);
			CancelCommand = new Command(OnCancel);
			this.PropertyChanged +=
				(_, __) => SaveCommand.ChangeCanExecute();
		}

		private bool ValidateSave()
		{
			return !string.IsNullOrEmpty(DescripcionZona);
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
			var newItem = ZonaDolor.New(DescripcionZona);
			var result = await _servicioApi.Add("ZonaDolor", newItem);
			IsBusy = false;
			if (result)
				await Shell.Current.GoToAsync("..");
		}

	
	}
}
