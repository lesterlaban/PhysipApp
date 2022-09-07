
namespace PhysipApp.Models
{
	public class Recurso : BaseModel
	{
		public int Id { get; set; }
		public string Titulo { get; set; }
		public string Descripcion { get; set; }
		public string Url { get; set; }
		public static Recurso New(string titulo, string descripcion, string url) => new Recurso()
		{
			Titulo= titulo,
			Descripcion = descripcion,
			Url = url,
			Estado = true
		};
	}
}
