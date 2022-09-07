using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace PhysipApp.ViewModels
{
	[QueryProperty(nameof(ItemId), nameof(ItemId))]
	public class PreguntaDetailViewModel : BaseViewModel
	{
		private string itemId;
		private string pregunta;

		public string Pregunta
		{
			get => pregunta;
			set => SetProperty(ref pregunta, value);
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
			try
			{
			}
			catch (Exception)
			{
				Debug.WriteLine("Failed to Load Item");
			}
		}
	}

	
}
