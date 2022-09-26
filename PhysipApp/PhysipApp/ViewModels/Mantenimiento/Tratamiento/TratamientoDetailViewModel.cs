using PhysipApp.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace PhysipApp.ViewModels
{
	[QueryProperty(nameof(ItemId), nameof(ItemId))]
	public class TratamientoDetailViewModel : BaseViewModel
	{
		public TratamientoDetailViewModel()
		{
			SaveCommand = new Command(OnSave);
			CancelCommand = new Command(OnCancel);
			this.PropertyChanged +=
			(_, __) => SaveCommand.ChangeCanExecute();
		}


		private string itemId;

		private Tratamiento _tratamiento;
		public Tratamiento TratamientoSelected
		{
			get => _tratamiento;
			set
			{
				SetProperty(ref _tratamiento, value);
			}
		}

		public string ItemId
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

		private ObservableCollection<object> _recursosSelected;
		public ObservableCollection<object> RecursosSelected
		{
			get => _recursosSelected;
			set
			{
				SetProperty(ref _recursosSelected, value);
			}
		}
		private ObservableCollection<Recurso> _recursos;
		public ObservableCollection<Recurso> Recursos
		{
			get => _recursos;
			set
			{
				SetProperty(ref _recursos, value);
			}
		}

		public async void LoadItemId(string itemId)
		{
			IsBusy = true;
			var item = await _servicioApi.GetItemAsync<Tratamiento>($"Tratamientos/{itemId}");
			IsBusy = false;
			if (item != null)
			{
				TratamientoSelected = item;
				Recursos = new ObservableCollection<Recurso>();
				var recursos = await _servicioApi.GetItemAsync<List<Recurso>>("Recursos");
				if (recursos != null)
					recursos.ForEach(i => Recursos.Add(i));

				RecursosSelected = new ObservableCollection<object>();
				Recursos.Where(r => item.Recursos.Select(s => s.IdRecurso).Contains(r.Id)).ToList().ForEach(r=> RecursosSelected.Add(r));
			}
		}


		public Command CancelCommand { get; }
		private async void OnCancel()
		{
			await Shell.Current.GoToAsync("..");
		}

		public Command SaveCommand { get; }
		private async void OnSave()
		{
			IsBusy = true;
			var selecionados = RecursosSelected.ToList().ConvertAll(x => (Recurso)x);
			TratamientoSelected.Recursos = selecionados.Select(r => new TratamientoRecurso() { IdRecurso = r.Id }).ToList();
			var result = await _servicioApi.Update("Tratamientos", TratamientoSelected);
			IsBusy = false;
			if (result)
				await Shell.Current.GoToAsync("..");
				
		}

	}


}
