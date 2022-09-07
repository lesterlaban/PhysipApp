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
	public class TratamientoViewModel : BaseViewModel
	{
		private Tratamiento _selectedItem;
		public ObservableCollection<Tratamiento> Items { get; }
		public Command LoadItemsCommand { get; }
		public Command AddItemCommand { get; }
		public Command<Tratamiento> ItemTapped { get; }


		public TratamientoViewModel()
		{
			Title = "Tratamientos";
			Items = new ObservableCollection<Tratamiento>();
			LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
			ItemTapped = new Command<Tratamiento>(OnItemSelected);
			AddItemCommand = new Command(OnAddItem);
			DeleteItem = new Command<Tratamiento>(_deleteItem);
			CerrarCommand = new Command(CerarSesion);
		}

		async Task ExecuteLoadItemsCommand()
		{
			IsBusy = true;
			Items.Clear();
			var items = await _servicioApi.GetItemAsync<List<Tratamiento>>("Tratamientos");
			if (items != null)
				items.ForEach(i => Items.Add(i));
			IsBusy = false;
		}

		public void OnAppearing()
		{
			IsBusy = true;
			SelectedItem = null;
		}

		public Tratamiento SelectedItem
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
			await Shell.Current.GoToAsync(nameof(NewTratamientoPage));
		}
		async void OnItemSelected(Tratamiento item)
		{
			if (item == null)
				return;

			await Shell.Current.GoToAsync($"{nameof(TratamientoDetailPage)}?{nameof(TratamientoDetailViewModel.ItemId)}={item.Id}");
		}

		public Command<Tratamiento> DeleteItem { get; }
		async void _deleteItem(Tratamiento item)
		{
			if (item == null)
				return;

			var question = await Application.Current.MainPage.DisplayAlert("Atención", 
				$"¿Está seguro de eliminar el tratamiento con zona {item.ZonaDolor.Descripcion} y nivel {item.NivelDolor.Descripcion}?", 
				"Aceptar", "Cancelar");
			if (question)
			{
				var result = await _servicioApi.Delete($"Tratamientos/{item.Id}");
				if (result)
					await ExecuteLoadItemsCommand();
			}
		}
	}
}