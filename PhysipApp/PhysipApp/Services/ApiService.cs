using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PhysipApp.Services
{
	public class ApiService : IApiService
    {
        protected string _host => "https://uptphysicapi.herokuapp.com/api";

        private async Task<T> InvokeAsyncApiFunction<T>(Func<HttpClient, Task<HttpResponseMessage>> funcion)
        {
            try
            {
                if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Application.Current.MainPage.DisplayAlert("Atención", "Revise su conexión a internet.", "OK");
                    return default(T);
                }
                HttpClientHandler clientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                };
                HttpClient httpClient = new HttpClient(clientHandler);
                var httpResponse = await funcion.Invoke(httpClient);

                if (httpResponse.IsSuccessStatusCode)
                {
                    string jsonString = await httpResponse.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(jsonString);
                }
                else if (httpResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    string jsonString = await httpResponse.Content.ReadAsStringAsync();
                    await Application.Current.MainPage.DisplayAlert("Atención", jsonString, "OK");
                }
                else if (httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    string jsonString = await httpResponse.Content.ReadAsStringAsync();
                    await Application.Current.MainPage.DisplayAlert("Atención", jsonString, "OK");
                }
                else
				{
                    await Application.Current.MainPage.DisplayAlert("Error", "Ha ocurrido un error, comuniquese con el administrador.", "OK");
                }
                return default(T);
            }
            catch (Exception excepcion)
            {
                await Application.Current.MainPage.DisplayAlert("Error", excepcion.Message, "OK");
                return default(T);
            }
        }

       

        public async Task<T> GetItemAsync<T>(string resource)
        {
            return await InvokeAsyncApiFunction<T>(async (httpClient) =>
            {
                return await httpClient.GetAsync($"{_host}/{resource}");
            });
        }


        public async Task<bool> Add(string resource, object content)
        {
            return await InvokeAsyncApiFunction<bool>(async (httpClient) =>
            {
                var json = JsonConvert.SerializeObject(content);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                return await httpClient.PostAsync($"{_host}/{resource}", stringContent);
            });
        }

        public async Task<bool> Delete(string resource)
        {
            return await InvokeAsyncApiFunction<bool>(async (httpClient) =>
            {
                var result = await httpClient.DeleteAsync($"{_host}/{resource}");
                if (result.IsSuccessStatusCode)
                    await Application.Current.MainPage.DisplayAlert("Atención", "Se eliminó correctamente.", "OK");

                return result;
            });
        }


    }
}
