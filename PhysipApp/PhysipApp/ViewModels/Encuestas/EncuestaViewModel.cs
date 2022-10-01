using PhysipApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PhysipApp.ViewModels
{
	public class EncuestaViewModel : BaseViewModel
	{
		public Command LoadItemsCommand { get; }

		private ObservableCollection<Encuesta> _encuestas;
		public ObservableCollection<Encuesta> Encuestas
		{
			get => _encuestas;
			set
			{
				SetProperty(ref _encuestas, value);
			}
		}

		private Encuesta _encuesta1;
		public Encuesta Encuesta1
		{
			get => _encuesta1;
			set
			{
				SetProperty(ref _encuesta1, value);
			}
		}
		private Encuesta _encuesta2;
		public Encuesta Encuesta2
		{
			get => _encuesta2;
			set
			{
				SetProperty(ref _encuesta2, value);
			}
		}
		private Encuesta _encuesta3;
		public Encuesta Encuesta3
		{
			get => _encuesta3;
			set
			{
				SetProperty(ref _encuesta3, value);
			}
		}

		private string _idUsuario;


		private bool _showEncuesta1;
		public bool showEncuesta1
		{
			get => _showEncuesta1;
			set
			{
				SetProperty(ref _showEncuesta1, value);
			}
		}


		private bool _showEncuesta2;
		public bool showEncuesta2
		{
			get => _showEncuesta2;
			set
			{
				SetProperty(ref _showEncuesta2, value);
			}
		}

		private bool _showEncuesta3;
		public bool showEncuesta3
		{
			get => _showEncuesta3;
			set
			{
				SetProperty(ref _showEncuesta3, value);
			}
		}

		public EncuestaViewModel()
		{
			Title = "Cuestionario";
			ExecuteLoadItemsCommand();
			showEncuesta1 = true;
			showEncuesta2 = false;
			showEncuesta3 = false;
			SaveCommand = new Command(OnSave);
			CerrarCommand= new Command(CerarSesion);
			GoToEncuesta1 = new Command(_GoToEncuesta1);
			GoToEncuesta2 = new Command(_GoToEncuesta2);
			GoToEncuesta3 = new Command(_GoToEncuesta3);
			this.PropertyChanged +=
				(_, __) => SaveCommand.ChangeCanExecute();
		}

		public Command GoToEncuesta1 { get; }
		private async void _GoToEncuesta1()
		{
			showEncuesta1 = true;
			showEncuesta2 = false;
			showEncuesta3 = false;
		}
		public Command GoToEncuesta2 { get; }
		private async void _GoToEncuesta2()
		{
			showEncuesta1 = false;
			showEncuesta2 = true;
			showEncuesta3 = false;
		}
		public Command GoToEncuesta3 { get; }
		private async void _GoToEncuesta3()
		{
			showEncuesta1 = false;
			showEncuesta2 = false;
			showEncuesta3 = true;
		}

		public Command SaveCommand { get; }
		private async void OnSave()
		{
			var contentPreguntas = new List<PreguntaUsuario>();
			contentPreguntas.AddRange(Encuesta1.PreguntasUsuario);
			contentPreguntas.AddRange(Encuesta2.PreguntasUsuario);
			contentPreguntas.AddRange(Encuesta3.PreguntasUsuario);

			if(contentPreguntas.Exists(p=> p.Puntaje == -1))
			{
				var preguntasNo = string.Join(" - ", contentPreguntas.Where(p => p.Puntaje == -1).Select(p => p.Pregunta.Descripcion));
				await Application.Current.MainPage.DisplayAlert("Atención", $"Seleccione: {preguntasNo}.", "OK");
				return;
			}
			IsBusy = true;
			var result = await _servicioApi.Add("Preguntas/Usuarios", contentPreguntas);
			if (result)
			{
				await Application.Current.MainPage.DisplayAlert("Atención", "Se ha guardado correctamente sus respuestas.", "OK");
				Application.Current.MainPage = new AppShellPaciente();
			}
			IsBusy = false;
		}

		async Task ExecuteLoadItemsCommand()
		{
			IsBusy = true;
			_idUsuario = await SecureStorage.GetAsync("idUser");

			Encuestas = new ObservableCollection<Encuesta>();
			var items = await _servicioApi.GetItemAsync<List<Encuesta>>("Encuestas");
			IsBusy = false;

			int group = 0;
			if (items != null)
			{
				items.ForEach(encuesta =>
				{
					var preguntas = encuesta.Secciones.SelectMany(s => s.Preguntas).ToList();
					encuesta.PreguntasUsuario = preguntas.Select(p => 
					{
						group++;
						return PreguntaUsuario.New(Convert.ToInt32(_idUsuario), p.Descripcion, p);
					} ).ToList();
					Encuestas.Add(encuesta);
					if (Encuesta1 == null)
						Encuesta1 = encuesta;
					else if (Encuesta2 == null)
						Encuesta2 = encuesta;
					else if (Encuesta3 == null)
						Encuesta3 = encuesta;
				});
			}
		}
				

		public void OnAppearing()
		{
			IsBusy = true;
		}

	}
}