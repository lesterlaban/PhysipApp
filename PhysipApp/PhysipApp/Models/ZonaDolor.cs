
namespace PhysipApp.Models
{
	public class ZonaDolor : BaseModel
	{
		public int Id { get; set; }
		public string Descripcion { get; set; }
		public static ZonaDolor New(string descripcion) => new ZonaDolor()
		{
			Descripcion = descripcion,
			Estado = true
		};
	}
}