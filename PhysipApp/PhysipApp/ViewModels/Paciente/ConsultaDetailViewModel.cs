using PhysipApp.Models;
using PhysipApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace PhysipApp.ViewModels
{
	[QueryProperty(nameof(ItemId), nameof(ItemId))]
	public class ConsultaDetailViewModel : BaseViewModel
	{
		public ConsultaDetailViewModel()
		{
			Title = "Tratamientos";
			ItemTapped = new Command<Recurso>(OnItemSelected);
		}
		public Command<Recurso> ItemTapped { get; }

		private ObservableCollection<Recurso> _items;
		public ObservableCollection<Recurso> Items
		{
			get => _items;
			set
			{
				SetProperty(ref _items, value);
			}
		}

		private Recurso _selectedItem;
		public Recurso SelectedItem
		{
			get => _selectedItem;
			set
			{
				SetProperty(ref _selectedItem, value);
				OnItemSelected(value);
			}
		}

		private int itemId;
		public int ItemId
		{
			get
			{
				return itemId;
			}
			set
			{
				itemId = value;
				LoadItemId(value);
			}
		}

		public async void LoadItemId(int itemId)
		{
			IsBusy = true;
			var item = await _servicioApi.GetItemAsync<RegistroConsulta>($"Consultas/{itemId}");
			if(item != null)
			{
				Title = $"{item.DescripcionNivel} - {item.DescripcionZona}";
				Items = new ObservableCollection<Recurso>();
				var recursos = await _servicioApi.GetItemAsync<List<Recurso>>($"Recursos/filters?idConsulta={item.Id}");
				if (recursos != null)
					recursos.ForEach(i => Items.Add(i));
			}		
			IsBusy = false;
		}

		async void OnItemSelected(Recurso item)
		{
			if (item == null)
				return;
			await Shell.Current.GoToAsync($"{nameof(ConsultaRecursoPage)}?{nameof(ConsultaRecursoModel.ItemId)}={item.Id}");
		}

	}

}
