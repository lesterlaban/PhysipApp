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
	public class NivelViewModel : BaseViewModel
	{
		private NivelDolor _selectedItem;
		public ObservableCollection<NivelDolor> Items { get; }
		public Command LoadItemsCommand { get; }
		public Command AddItemCommand { get; }

		public NivelViewModel()
		{
			Title = "Niveles de Dolor";
			Items = new ObservableCollection<NivelDolor>();
			LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
			DeleteItem = new Command<NivelDolor>(_deleteItem);
			AddItemCommand = new Command(OnAddItem);
			CerrarCommand = new Command(CerarSesion);
		}

		async Task ExecuteLoadItemsCommand()
		{
			IsBusy = true;
			Items.Clear();
			var items = await _servicioApi.GetItemAsync<List<NivelDolor>>("NivelDolor");
			if(items != null)
				items.ForEach(i => Items.Add(i));
			IsBusy = false;
		}

		public void OnAppearing()
		{
			IsBusy = true;
			SelectedItem = null;
		}

		public NivelDolor SelectedItem
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
			await Shell.Current.GoToAsync(nameof(NewNivelPage));
		}

		public Command<NivelDolor> DeleteItem { get; }

		async void _deleteItem(NivelDolor item)
		{
			if (item == null)
				return;
			var question = await Application.Current.MainPage.DisplayAlert("Atención", $"¿Está seguro de eliminar el nivel {item.Descripcion}?", "Aceptar", "Cancelar");
			if(question)
			{
				var result = await _servicioApi.Delete($"NivelDolor/{item.Id}");
				if (result)
					await ExecuteLoadItemsCommand();
			}
		}
	}
}