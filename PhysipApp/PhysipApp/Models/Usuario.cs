using System.Collections.Generic;
using System.Linq;

namespace PhysipApp.Models
{
	public class Usuario
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public string Contrasenia { get; set; }
		public bool Estado { get; set; }
		public int IdRol { get; set; }
		public List<PreguntaUsuario> PreguntaUsuario { get; set; }
		public List<SeccionUsuario> SeccionUsuario { get; set; }
		public bool TieneEncuesta { get; set; }
		public Rol Rol { get; set; }
		public static Usuario New(string nombre, string contrasenia) => new Usuario()
		{
			Nombre = nombre,
			Contrasenia = contrasenia,
			Estado = true,
			IdRol = 2,
		};
	}
}
