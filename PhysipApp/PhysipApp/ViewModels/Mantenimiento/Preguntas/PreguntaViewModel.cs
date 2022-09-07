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
	public class PreguntaViewModel : BaseViewModel
	{
		private Pregunta _selectedItem;
		public ObservableCollection<Pregunta> Items { get; }
		public Command LoadItemsCommand { get; }
		public Command AddItemCommand { get; }


		public PreguntaViewModel()
		{
			Title = "Preguntas";
			Items = new ObservableCollection<Pregunta>();
			LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
			DeleteItem = new Command<Pregunta>(_deleteItem);
			AddItemCommand = new Command(OnAddItem);
			CerrarCommand = new Command(CerarSesion);
		}

		async Task ExecuteLoadItemsCommand()
		{
			IsBusy = true;
			Items.Clear();
			var items = await _servicioApi.GetItemAsync<List<Pregunta>>("Preguntas");
			if (items != null)
				items.ForEach(i => Items.Add(i));
			IsBusy = false;
		}

		public void OnAppearing()
		{
			IsBusy = true;
			SelectedItem = null;
		}

		public Pregunta SelectedItem
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
			await Shell.Current.GoToAsync(nameof(NewPreguntaPage));
		}

		public Command<Pregunta> DeleteItem { get; }

		async void _deleteItem(Pregunta item)
		{
			if (item == null)
				return;

			var question = await Application.Current.MainPage.DisplayAlert("Atención", $"¿Está seguro de eliminar la pregunta {item.Descripcion}?", "Aceptar", "Cancelar");
			if (question)
			{
				var result = await _servicioApi.Delete($"Preguntas/{item.Id}");
				if (result)
					await ExecuteLoadItemsCommand();
			}
		}
	}
}