using PhysipApp.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace PhysipApp.ViewModels
{
	public class NewPreguntaViewModel : BaseViewModel
	{
		private string _descripcionPregunta;
		public string DescripcionPregunta
		{
			get => _descripcionPregunta;
			set
			{
				SetProperty(ref _descripcionPregunta, value);
			}
		}

		private ObservableCollection<Encuesta> _encuestas;
		public ObservableCollection<Encuesta> Encuestas
		{
			get => _encuestas;
			set
			{
				SetProperty(ref _encuestas, value);
			}
		}
		private Encuesta _encuestaSeleccionada;
		public Encuesta EncuestaSeleccionada
		{
			get => _encuestaSeleccionada;
			set
			{
				SetProperty(ref _encuestaSeleccionada, value);
			}
		}
		private ObservableCollection<EncuestaSeccion> _secciones;
		public ObservableCollection<EncuestaSeccion> Secciones
		{
			get => _secciones;
			set
			{
				SetProperty(ref _secciones, value);
			}
		}
		private EncuestaSeccion _seccionSeleccionada;
		public EncuestaSeccion SeccionSeleccionada
		{
			get => _seccionSeleccionada;
			set
			{
				SetProperty(ref _seccionSeleccionada, value);
			}
		}

		public Command AddItemCommand { get; }
		public NewPreguntaViewModel()
		{
			Title = "Registro de Preguntas";
			SaveCommand = new Command(OnSave, ValidateSave);
			CancelCommand = new Command(OnCancel);
			this.PropertyChanged +=
				(_, __) => SaveCommand.ChangeCanExecute();

			ObtenerDatos();

			ChangeEncuestaCommand = new Command<Encuesta>(_changeEncuestaCommand);
		}
		private async void ObtenerDatos()
		{
			IsBusy = true;
			Encuestas = new ObservableCollection<Encuesta>();
			var recursos = await _servicioApi.GetItemAsync<List<Encuesta>>("Encuestas");
			if (recursos != null)
				recursos.ForEach(i => Encuestas.Add(i));

			IsBusy = false;
		}
		private bool ValidateSave()
		{
			return !string.IsNullOrEmpty(DescripcionPregunta) && SeccionSeleccionada != null;
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
			var newItem = Pregunta.New(DescripcionPregunta, SeccionSeleccionada);
			var result = await _servicioApi.Add("Preguntas", newItem);
			IsBusy = false;
			if (result)
				await Shell.Current.GoToAsync("..");
		}

		public Command<Encuesta> ChangeEncuestaCommand { get; }

		async void _changeEncuestaCommand(Encuesta item)
		{
			if (item == null)
				return;

			Secciones = new ObservableCollection<EncuestaSeccion>();
			item.Secciones?.ForEach(s=> Secciones.Add(s));
		}

	}
}
