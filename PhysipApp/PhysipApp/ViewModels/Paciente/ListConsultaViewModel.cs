using PhysipApp.Models;
using PhysipApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace PhysipApp.ViewModels
{
	public class ListConsultaViewModel : BaseViewModel
	{
		private RegistroConsulta _selectedItem;
		public ObservableCollection<RegistroConsulta> Items { get; }
		public Command LoadItemsCommand { get; }
		public Command AddItemCommand { get; }
		public Command<RegistroConsulta> ItemTapped { get; }
		public ListConsultaViewModel()
		{
			Title = "Historial";
			Items = new ObservableCollection<RegistroConsulta>();
			LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
			ItemTapped = new Command<RegistroConsulta>(OnItemSelected);
			AddItemCommand = new Command(OnAddItem);
		}

		async Task ExecuteLoadItemsCommand()
		{
			IsBusy = true;
			var idUsuario = await SecureStorage.GetAsync("idUser");
			Items.Clear();
			var items = await _servicioApi.GetItemAsync<List<RegistroConsulta>>($"Consultas/filters?idUsuario={idUsuario}");
			if (items != null)
				items.ForEach(i => Items.Add(i));
			IsBusy = false;
		}

		public void OnAppearing()
		{
			IsBusy = true;
			SelectedItem = null;
		}

		public RegistroConsulta SelectedItem
		{
			get => _selectedItem;
			set
			{
				SetProperty(ref _selectedItem, value);
				OnItemSelected(value);
			}
		}

		private async void OnAddItem(object obj)
		{
			//await Shell.Current.GoToAsync(nameof(NewItemPage));
		}

		async void OnItemSelected(RegistroConsulta item)
		{
			if (item == null)
				return;

			await Shell.Current.GoToAsync($"{nameof(ConsultaDetailPage)}?{nameof(ConsultaDetailViewModel.ItemId)}={item.Id}");
		}
	}
}