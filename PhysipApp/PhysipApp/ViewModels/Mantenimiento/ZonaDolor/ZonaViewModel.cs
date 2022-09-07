using PhysipApp.Models;
using PhysipApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PhysipApp.ViewModels
{
	public class ZonaViewModel : BaseViewModel
	{
		private ZonaDolor _selectedItem;
		public ObservableCollection<ZonaDolor> Items { get; }
		public Command LoadItemsCommand { get; }
		public Command AddItemCommand { get; }


		public ZonaViewModel()
		{
			Title = "Zonas";
			Items = new ObservableCollection<ZonaDolor>();
			LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
			DeleteItem = new Command<ZonaDolor>(_deleteItem);
			AddItemCommand = new Command(OnAddItem);
			CerrarCommand = new Command(CerarSesion);
		}

		async Task ExecuteLoadItemsCommand()
		{
			IsBusy = true;
			Items.Clear();
			var items = await _servicioApi.GetItemAsync<List<ZonaDolor>>("ZonaDolor");
			if (items != null)
				items.ForEach(i => Items.Add(i));
			IsBusy = false;
		}

		public void OnAppearing()
		{
			IsBusy = true;
			SelectedItem = null;
		}

		public ZonaDolor SelectedItem
		{
			get => _selectedItem;
			set
			{
				SetProperty(ref _selectedItem, value);
				_deleteItem(value);
			}
		}

		private async void OnAddItem(object obj)
		{
			await Shell.Current.GoToAsync(nameof(NewZonaPage));
		}

		public Command<ZonaDolor> DeleteItem { get; }

		async void _deleteItem(ZonaDolor item)
		{
			if (item == null)
				return;

			var question = await Application.Current.MainPage.DisplayAlert("Atención", $"¿Está seguro de eliminar la zona {item.Descripcion}?", "Aceptar", "Cancelar");
			if (question)
			{
				var result = await _servicioApi.Delete($"ZonaDolor/{item.Id}");
				if (result)
					await ExecuteLoadItemsCommand();
			}
		}
	}
}