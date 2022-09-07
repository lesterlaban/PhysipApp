using PhysipApp.Models;
using PhysipApp.Views;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using XCalendar.Core.Models;
using XCalendar.Core.Enums;
using PhysipApp.Models.Calendar;
using Xamarin.Essentials;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhysipApp.ViewModels
{
	public class ResultadoEncuestaViewModel : BaseViewModel
	{
		public Command LoadItemsCommand { get; }

		private ObservableCollection<SeccionUsuario> _items;
		public ObservableCollection<SeccionUsuario> Items
		{
			get => _items;
			set
			{
				SetProperty(ref _items, value);
			}
		}

		public ResultadoEncuestaViewModel()
		{
			Title = "Bienvenido";
			LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
			CerrarCommand = new Command(CerarSesion);
		}

		async Task ExecuteLoadItemsCommand()
		{
			IsBusy = true;
			var idUsuario = await SecureStorage.GetAsync("idUser");
			var usuario = await _servicioApi.GetItemAsync<Usuario>($"Usuarios/{idUsuario}");
			Items = new ObservableCollection<SeccionUsuario>();
			usuario.SeccionUsuario.ForEach(s => Items.Add(s));
			IsBusy = false;
		}


		public void OnAppearing()
		{
			IsBusy = true;
		}


	}
}