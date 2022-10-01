using PhysipApp.Models;
using Xamarin.Forms;

namespace PhysipApp.ViewModels
{
	public class NewNivelViewModel : BaseViewModel
	{

		private string _descripcionNivel;
		public string DescripcionNivel
		{
			get => _descripcionNivel;
			set
			{
				SetProperty(ref _descripcionNivel, value);
			}
		}

		public NewNivelViewModel()
		{
			Title = "Registro de Niveles";
			SaveCommand = new Command(OnSave, ValidateSave);
			CancelCommand = new Command(OnCancel);
			this.PropertyChanged +=
				(_, __) => SaveCommand.ChangeCanExecute();
		}

		private bool ValidateSave()
		{
			return !string.IsNullOrEmpty(DescripcionNivel);
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
			var newItem = NivelDolor.New(DescripcionNivel);
			IsBusy = true;
			var result = await _servicioApi.Add("NivelDolor", newItem);
			IsBusy = false;
			if (result)
				await Shell.Current.GoToAsync("..");
		}

	
	}
}
