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

namespace PhysipApp.ViewModels
{
	public class ConsultaViewModel : BaseViewModel
	{
		public Calendar<EventDay> EventCalendar { get; set; } = new Calendar<EventDay>()
		{
			SelectedDates = new ObservableRangeCollection<DateTime>(),
			SelectionAction = SelectionAction.Modify,
			SelectionType = SelectionType.Single
		};

		public ObservableCollection<RegistroConsulta> Items { get; }
		public Command Buscar { get; }
		public Command AddItemCommand { get; }
	
		public Command NavigateCalendarCommand { get; set; }
		public Command NavigateCalendarBackCommand { get; set; }
		public Command ChangeDateSelectionCommand { get; set; }
		private RegistroConsulta _selectedItem;
		public RegistroConsulta SelectedItem
		{
			get => _selectedItem;
			set
			{
				SetProperty(ref _selectedItem, value);
				OnItemSelected(value);
			}
		}
		public Command<RegistroConsulta> ItemTapped { get; }
		public ConsultaViewModel()
		{
			Title = "Bienvenido";
			NavigateCalendarCommand = new Command<int>(NavigateCalendar);
			NavigateCalendarBackCommand = new Command<int>(NavigateCalendarBack);
			ChangeDateSelectionCommand = new Command<DateTime>(ChangeDateSelection);
			Buscar = new Command(BuscarTratamientoPorFecha);
			ItemTapped = new Command<RegistroConsulta>(OnItemSelected);
			AddItemCommand = new Command(OnAddItem);
			CerrarCommand = new Command(CerarSesion);
			Items = new ObservableCollection<RegistroConsulta>();
		}
	
		private async void BuscarTratamientoPorFecha()
		{
			IsBusy = true;
			Console.WriteLine(EventCalendar);
			if(EventCalendar.SelectedDates.Any())
			{
				Items.Clear();
				var idUsuario = await SecureStorage.GetAsync("idUser");
				string url = $"Consultas/filters?idUsuario={idUsuario}";
				EventCalendar.SelectedDates.ToList().ForEach(s=> url += $"&fechas={s.ToString("yyyy-MM-dd")}");
				var items = await _servicioApi.GetItemAsync<List<RegistroConsulta>>(url);
				if (items != null)
					items.ForEach(i => Items.Add(i));
			}
			else
				await Application.Current.MainPage.DisplayAlert("Atención", "Seleccione al menos una fecha para visualizar sus consultas.", "OK");

			IsBusy = false;
		}


		private async void OnAddItem(object obj)
		{
			await Shell.Current.GoToAsync(nameof(NewConsultaPage));
		}

		public void NavigateCalendarBack(int Amount)
		{
			EventCalendar?.NavigateCalendar(-1);
		}
		public void NavigateCalendar(int Amount)
		{
			EventCalendar?.NavigateCalendar(Amount);
		}
		public void ChangeDateSelection(DateTime DateTime)
		{
			EventCalendar?.ChangeDateSelection(DateTime);
		}

		async void OnItemSelected(RegistroConsulta item)
		{
			if (item == null)
				return;

			await Shell.Current.GoToAsync($"{nameof(ConsultaDetailPage)}?{nameof(ConsultaDetailViewModel.ItemId)}={item.Id}");
		}

	}
}