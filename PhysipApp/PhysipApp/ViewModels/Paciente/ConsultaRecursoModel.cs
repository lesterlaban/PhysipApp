using PhysipApp.Models;
using Xamarin.Forms;

namespace PhysipApp.ViewModels
{
	[QueryProperty(nameof(ItemId), nameof(ItemId))]
	public class ConsultaRecursoModel : BaseViewModel
	{

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

		private string _url;
		public string Url
		{
			get => _url;
			set
			{
				SetProperty(ref _url, value);
			}
		}

		public async void LoadItemId(int itemId)
		{
			IsBusy = true;
			var item = await _servicioApi.GetItemAsync<Recurso>($"Recursos/{itemId}");
			if (item != null)
			{
				Title = $"{item.Titulo}";
				Url = item.Url;
			}
			IsBusy = false;
		}

	}
}
