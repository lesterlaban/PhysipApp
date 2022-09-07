
namespace PhysipApp.Models
{
	public class NivelDolor : BaseModel
	{
		public int Id { get; set; }
		public string Descripcion { get; set; }
		public static NivelDolor New(string descripcion) => new NivelDolor() 
		{ 
			Descripcion = descripcion, Estado = true
		};
	}
}