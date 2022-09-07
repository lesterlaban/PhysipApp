using PhysipApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PhysipApp.ViewModels
{
	public class NewConsultaViewModel : BaseViewModel
	{
		public NewConsultaViewModel()
		{
			Title = "Registro de Consultas";
			ObtenerDatos();
			SaveCommand = new Command(OnSave, ValidateSave);
			CancelCommand = new Command(OnCancel);
			this.PropertyChanged +=
			(_, __) => SaveCommand.ChangeCanExecute();
		}


		private ObservableCollection<ZonaDolor> _zonas;
		public ObservableCollection<ZonaDolor> Zonas
		{
			get => _zonas;
			set
			{
				SetProperty(ref _zonas, value);
			}
		}
		private ObservableCollection<NivelDolor> _niveles;
		public ObservableCollection<NivelDolor> Niveles
		{
			get => _niveles;
			set
			{
				SetProperty(ref _niveles, value);
			}
		}

					
		private ZonaDolor _zonaSeleccionada;
		public ZonaDolor ZonaSeleccionada
		{
			get => _zonaSeleccionada;
			set
			{
				SetProperty(ref _zonaSeleccionada, value);
			}
		}

		private NivelDolor _nivelSeleccionado;
		public NivelDolor NivelSeleccionado
		{
			get => _nivelSeleccionado;
			set
			{
				SetProperty(ref _nivelSeleccionado, value);
			}
		}

	
		private bool ValidateSave()
		{
			return ZonaSeleccionada != null
				&& NivelSeleccionado != null;
		}


		public Command CancelCommand { get; }
		private async void OnCancel()
		{
			await Shell.Current.GoToAsync("..");
		}

		public Command SaveCommand { get; }
		private async void OnSave()
		{
			IsBusy = true;
			var idUsuario = await SecureStorage.GetAsync("idUser");
			var newItem = RegistroConsulta.New(Convert.ToInt32(idUsuario), ZonaSeleccionada, NivelSeleccionado);
			var result = await _servicioApi.Add("Consultas", newItem);
			IsBusy = false;
			if (result)
				await Shell.Current.GoToAsync("..");
		}

		private async void ObtenerDatos()
		{
			IsBusy = true;
			Zonas = new ObservableCollection<ZonaDolor>();
			var items = await _servicioApi.GetItemAsync<List<ZonaDolor>>("ZonaDolor");
			if (items != null)
				items.ForEach(i => Zonas.Add(i));

			Niveles = new ObservableCollection<NivelDolor>();
			var niveles = await _servicioApi.GetItemAsync<List<NivelDolor>>("NivelDolor");
			if (niveles != null)
				niveles.ForEach(i => Niveles.Add(i));
			IsBusy = false;
		}

	}
}
