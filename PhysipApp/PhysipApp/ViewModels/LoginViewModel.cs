using PhysipApp.Models;
using PhysipApp.Views;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PhysipApp.ViewModels
{
	public class LoginViewModel : BaseViewModel
	{
		public Command LoginCommand { get; }
        public Command NuevoUsuario { get; }

        public LoginViewModel()
		{
			LoginCommand = new Command(OnLoginClicked);
            NuevoUsuario = new Command(NuevoUsuarioClicked);
        }

		private async void OnLoginClicked(object obj)
		{
            IsBusy = true;
			var usuario = await _servicioApi.GetItemAsync<Usuario>($"Usuarios/filters?usuario={Usuario}&password={Password}");
			IsBusy = false;
			if (usuario != null)
			{
				await SecureStorage.SetAsync("idUser", usuario?.Id.ToString());
				if (usuario.IdRol == 1)
					Application.Current.MainPage = new AppShell();
				else
				{
					if (usuario.TieneEncuesta)
                        Application.Current.MainPage = new AppShellPaciente();
                    else
                        Application.Current.MainPage = new AppShellEncuesta();
                }
            }
		}

        private async void NuevoUsuarioClicked(object obj)
        {
            Application.Current.MainPage = new AppShellNewUsuario();
        }

        private string _usuario;
        public string Usuario
        {
            get => _usuario;
            set
            {
                SetProperty(ref _usuario, value);
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                SetProperty(ref _password, value);
            }
        }

   
    }
}
