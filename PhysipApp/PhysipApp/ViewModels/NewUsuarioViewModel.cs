using PhysipApp.Views;
using Xamarin.Forms;

namespace PhysipApp.ViewModels
{
	public class NewUsuarioViewModel : BaseViewModel
	{
        public Command RegistrarUsuario { get; }
        public Command Volver { get; }

        public NewUsuarioViewModel()
		{
            RegistrarUsuario = new Command(RegistrarUsuarioClicked);
            Volver = new Command(VolverClicked);
        }


        private async void RegistrarUsuarioClicked(object obj)
        {
            if(Password == PasswordRepet)
			{
                IsBusy = true;
                var newItem = Models.Usuario.New(Usuario, Password);
                var result = await _servicioApi.Add("Usuarios", newItem);
                IsBusy = false;
                if (result)
                {
                    await Application.Current.MainPage.DisplayAlert("Atención", $"Se guardo correctamente el nuevo usuario {Usuario}", "Ok");
                    Application.Current.MainPage = new AppShellLogin();
                }
            }
            else
                await Application.Current.MainPage.DisplayAlert("Atención", $"Las contraseñas no coinciden.", "Ok");
        }

        private async void VolverClicked(object obj)
        {
            Application.Current.MainPage = new AppShellLogin();
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

        private string _passwordRepet;
        public string PasswordRepet
        {
            get => _passwordRepet;
            set
            {
                SetProperty(ref _passwordRepet, value);
            }
        }


    }
}
