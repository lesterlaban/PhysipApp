using PhysipApp.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace PhysipApp.ViewModels
{
	public class NewTratamientoViewModel : BaseViewModel
	{
		public NewTratamientoViewModel()
		{
			Title = "Registro de Tratamiento";
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

		private ObservableCollection<EncuestaSeccion> _encuestas;
		public ObservableCollection<EncuestaSeccion> Encuestas
		{
			get => _encuestas;
			set
			{
				SetProperty(ref _encuestas, value);
			}
		}


		private ObservableCollection<Recurso> _recursos;
		public ObservableCollection<Recurso> Recursos
		{
			get => _recursos;
			set
			{
				SetProperty(ref _recursos, value);
			}
		}


		private ObservableCollection<object> _recursosSelected;
		public ObservableCollection<object> RecursosSelected
		{
			get => _recursosSelected;
			set
			{
				SetProperty(ref _recursosSelected, value);
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

		private EncuestaSeccion _encuestaSeleccionada;
		public EncuestaSeccion EncuestaSeleccionada
		{
			get => _encuestaSeleccionada;
			set
			{
				SetProperty(ref _encuestaSeleccionada, value);
			}
		}

		private Recurso _recursoSeleccionado;
		public Recurso RecursoSeleccionado
		{
			get => _recursoSeleccionado;
			set
			{
				SetProperty(ref _recursoSeleccionado, value);
			}
		}


		private int _puntajeMinimo;
		public int PuntajeMinimo
		{
			get => _puntajeMinimo;
			set
			{
				SetProperty(ref _puntajeMinimo, value);
			}
		}
		private int _puntajeMaximo;
		public int PuntajeMaximo
		{
			get => _puntajeMaximo;
			set
			{
				SetProperty(ref _puntajeMaximo, value);
			}
		}

		private bool ValidateSave()
		{
			return ZonaSeleccionada != null
				&& NivelSeleccionado != null
				&& EncuestaSeleccionada != null;
		}


		public Command CancelCommand { get; }
		private async void OnCancel()
		{
			await Shell.Current.GoToAsync("..");
		}

		public Command SaveCommand { get; }
		private async void OnSave()
		{
			var selecionados = RecursosSelected.ToList().ConvertAll(x => (Recurso)x);
			var newItem = Tratamiento.New(ZonaSeleccionada, NivelSeleccionado, 
				EncuestaSeleccionada, PuntajeMinimo, PuntajeMaximo, selecionados);
			IsBusy = true;
			var result = await _servicioApi.Add("Tratamientos", newItem);
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

			Encuestas = new ObservableCollection<EncuestaSeccion>();
			var encuestas = await _servicioApi.GetItemAsync<List<Encuesta>>("Encuestas");
			var encuestasSeccion = encuestas.SelectMany(e=> e.Secciones).ToList();

			if (encuestas != null)
				encuestasSeccion.ForEach(i => Encuestas.Add(i));

			Recursos = new ObservableCollection<Recurso>();
			var recursos = await _servicioApi.GetItemAsync<List<Recurso>>("Recursos");
			if (recursos != null)
				recursos.ForEach(i => Recursos.Add(i));

			RecursosSelected = new ObservableCollection<object>();

			IsBusy = false;
		}

	}
}
