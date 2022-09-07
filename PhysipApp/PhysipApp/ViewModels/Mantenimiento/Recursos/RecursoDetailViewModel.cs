using PhysipApp.Models;
using Xamarin.Forms;

namespace PhysipApp.ViewModels
{
	[QueryProperty(nameof(ItemId), nameof(ItemId))]
	public class RecursoDetailViewModel : BaseViewModel
	{
		private string itemId;
		private string _titulo;
		private string _descripcion;
		private string _url;

		public string Titulo
		{
			get => _titulo;
			set => SetProperty(ref _titulo, value);
		}

		public string Description
		{
			get => _descripcion;
			set => SetProperty(ref _descripcion, value);
		}
		public string Url
		{
			get => _url;
			set => SetProperty(ref _url, value);
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

		public async void LoadItemId(string itemId)
		{
			IsBusy = true;
			var item = await _servicioApi.GetItemAsync<Recurso>($"Recursos/{itemId}");
			IsBusy = false;
			if(item != null)
			{
				Titulo = item.Titulo;
				Description = item.Descripcion;
				Url = item.Url;
			}
		}
	}

}
