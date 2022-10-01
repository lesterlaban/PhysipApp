using System.Linq;
using System.Collections.Generic;

namespace PhysipApp.Models
{
	public class Tratamiento : BaseModel
	{
		public int Id { get; set; }
		public int IdZona { get; set; }
		public int IdNivelDolor { get; set; }
		public int IdEncuestaSeccion { get; set; }
		public string DescripcionZona => $"{ZonaDolor?.Descripcion}";
		public string DescripcionNivel => $"Nivel {NivelDolor?.Descripcion}";
		public string DescripcionEncuesta => $"{EncuestaSeccion?.Encuesta?.Nombre} {EncuestaSeccion?.Nombre}";
		public string DescripcionPuntaje => $"Rango Puntaje {PuntajeMinimo} - {PuntajeMaximo}";
		public ZonaDolor ZonaDolor { get; set; }
		public NivelDolor NivelDolor { get; set; }
		public EncuestaSeccion EncuestaSeccion { get; set; }
		public virtual List<TratamientoRecurso> Recursos { get; set; }
		public int PuntajeMinimo { get; set; }
		public int PuntajeMaximo { get; set; }

		public static Tratamiento New(ZonaDolor zona, 
			NivelDolor nivel, EncuestaSeccion EecuestaSeccion,
			int minimo, int maximo, List<Recurso> recursos) => new Tratamiento()
		{
			IdZona = zona.Id,
			IdNivelDolor = nivel.Id,
			IdEncuestaSeccion = EecuestaSeccion.Id,
			PuntajeMinimo = minimo,
			PuntajeMaximo = maximo,
			Recursos = recursos.Select(r => new TratamientoRecurso() { IdRecurso = r.Id}).ToList(),
			Estado = true,
		};
	}

	public class TratamientoRecurso
	{
		public int Id { get; set; }
		public int IdTratamiento { get; set; }
		public int IdRecurso { get; set; }
		public virtual Tratamiento Tratamiento { get; set; }
		public virtual Recurso Recurso { get; set; }
	}

}
