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

		public EncuestaViewModel()
		{
			Title = "Cuestionario";
			ExecuteLoadItemsCommand();
			SaveCommand = new Command(OnSave);
			CerrarCommand= new Command(CerarSesion);
			this.PropertyChanged +=
				(_, __) => SaveCommand.ChangeCanExecute();
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
				await Application.Current.MainPage.DisplayAlert("Atención", "Seleccione todas las preguntas del cuestionario.", "OK");
				return;
			}

			var result = await _servicioApi.Add("Preguntas/Usuarios", contentPreguntas);
			if (result)
			{
				await Application.Current.MainPage.DisplayAlert("Atención", "Se ha guardado correctamente sus respuestas.", "OK");
				Application.Current.MainPage = new AppShellPaciente();
			}
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
						return PreguntaUsuario.New(Convert.ToInt32(_idUsuario), group.ToString(), p);
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