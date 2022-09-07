using System;

namespace PhysipApp.Models
{
	public class RegistroConsulta
	{
		public int Id { get; set; }
		public int IdUsuario { get; set; }
		public int IdZona { get; set; }
		public int IdNivelDolor { get; set; }
		public int PuntajeMinimo { get; set; }
		public DateTime Fecha { get; set; }
		public bool Estado { get; set; }
		public Usuario Usuario { get; set; }
		public ZonaDolor ZonaDolor { get; set; }
		public NivelDolor NivelDolor { get; set; }		

		public string DescripcionZona => $"Zona: {ZonaDolor?.Descripcion}";
		public string DescripcionNivel => $"Nivel de Dolor: {NivelDolor?.Descripcion}";
		public string DescripcionFecha => $"Fecha: {Fecha.ToString("dd-MM-yyyy HH:mm:ss")}";
		public static RegistroConsulta New(int idUsuario, ZonaDolor zonaDolor, NivelDolor nivelDolor) => new RegistroConsulta()
		{
			IdUsuario = idUsuario,
			Fecha = DateTime.Now,
			IdZona= zonaDolor.Id,
			IdNivelDolor = nivelDolor.Id,
			Estado = true
		};
	}
}