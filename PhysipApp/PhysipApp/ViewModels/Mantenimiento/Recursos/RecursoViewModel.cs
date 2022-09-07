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
	public class RecursoViewModel : BaseViewModel
	{
		private Recurso _selectedItem;
		public ObservableCollection<Recurso> Items { get; }
		public Command LoadItemsCommand { get; }
		public Command AddItemCommand { get; }
		public Command<Recurso> ItemTapped { get; }


		public RecursoViewModel()
		{
			Title = "Recursos";
			Items = new ObservableCollection<Recurso>();
			LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
			ItemTapped = new Command<Recurso>(OnItemSelected);
			AddItemCommand = new Command(OnAddItem);
			DeleteItem = new Command<Recurso>(_deleteItem);
			CerrarCommand = new Command(CerarSesion);
		}

		async Task ExecuteLoadItemsCommand()
		{
			IsBusy = true;
			Items.Clear();
			var items = await _servicioApi.GetItemAsync<List<Recurso>>("Recursos");
			if (items != null)
				items.ForEach(i => Items.Add(i));
			IsBusy = false;
		}

		public void OnAppearing()
		{
			IsBusy = true;
			SelectedItem = null;
		}

		public Recurso SelectedItem
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
			await Shell.Current.GoToAsync(nameof(NewRecursoPage));
		}

		async void OnItemSelected(Recurso item)
		{
			if (item == null)
				return;

			await Shell.Current.GoToAsync($"{nameof(RecursoDetailPage)}?{nameof(RecursoDetailViewModel.ItemId)}={item.Id}");
		}

		public Command<Recurso> DeleteItem { get; }
		async void _deleteItem(Recurso item)
		{
			if (item == null)
				return;

			var question = await Application.Current.MainPage.DisplayAlert("Atención", $"¿Está seguro de eliminar el recurso {item.Titulo}?", "Aceptar", "Cancelar");
			if (question)
			{
				var result = await _servicioApi.Delete($"Recursos/{item.Id}");
				if (result)
					await ExecuteLoadItemsCommand();
			}
		}

	}
}