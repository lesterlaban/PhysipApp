using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhysipApp.Services
{
	public interface IApiService
	{
		Task<T> GetItemAsync<T>(string resource);
		Task<bool> Add(string resource, object content);
		Task<bool> Delete(string resource);
	}
}
