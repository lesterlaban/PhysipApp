using PhysipApp.ViewModels;
using System.Collections.Generic;

namespace PhysipApp.Models
{
	public class Encuesta
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public bool Estado { get; set; }
		public List<EncuestaSeccion> Secciones { get; set; }
		public string NombreDisplay => $"Encuesta: {Nombre}";
		public List<PreguntaUsuario> PreguntasUsuario { get; set; }
	}

	public class EncuestaSeccion
	{
		public int Id { get; set; }
		public int IdEncuesta { get; set; }
		public string Nombre { get; set; }
		public string Indicadores { get; set; }
		public bool Estado { get; set; }
		public Encuesta Encuesta { get; set; }
		public List<RangoSeccion> Rangos { get; set; }
		public List<Pregunta> Preguntas { get; set; }
		public List<SeccionUsuario> SeccionUsuario { get; set; }
		public string NombreDisplay => $"Sección: {Nombre}";
		public RangoSeccion RangoValido { get; set; }
	}

	public class RangoSeccion
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public int IdEncuestaSeccion { get; set; }
		public int ValorMinimo { get; set; }
		public int ValorMaximo { get; set; }
		public bool Estado { get; set; }
		public EncuestaSeccion Seccion { get; set; }
	}

	public class Pregunta
	{
		public int Id { get; set; }
		public string Descripcion { get; set; }
		public bool Estado { get; set; }
		public int IdEncuestaSeccion { get; set; }
		public EncuestaSeccion Seccion { get; set; }
		public List<PreguntaUsuario> PreguntaUsuario { get; set; }
		public string NombreDisplay => $"Pregunta: {Descripcion}";
		public static Pregunta New(string descripcion, EncuestaSeccion seccion)
		{
			return new Pregunta() 
			{
				Descripcion = descripcion,
				Seccion = seccion,
				IdEncuestaSeccion = seccion.Id,
				Estado = true,
			};
		}
	}

	public class PreguntaUsuario
	{
		public int Id { get; set; }
		public int IdPregunta { get; set; }
		public int IdUsuario { get; set; }
		public int Puntaje { get; set; }
		public bool Estado { get; set; }
		public Pregunta Pregunta { get; set; }
		public Usuario Usuario { get; set; }
		public string GrupoButton { get; set; }
		public bool RespuestaSeleccionada => Puntaje <= -1;
		public static PreguntaUsuario New(int idUsuario, string groupName, Pregunta pregunta)
		{
			return new PreguntaUsuario()
			{
				IdUsuario = idUsuario,
				IdPregunta = pregunta.Id,
				Pregunta = pregunta,
				GrupoButton = groupName,
				Estado = true,
				Puntaje = -1,
			};
		}
	}

	public class SeccionUsuario
	{
		public int Id { get; set; }
		public int IdEncuestaSeccion { get; set; }
		public int IdUsuario { get; set; }
		public int Puntaje { get; set; }
		public bool Estado { get; set; }
		public EncuestaSeccion Seccion { get; set; }
		public Usuario Usuario { get; set; }
		public string Titulo => $"{Seccion.Encuesta.Nombre} - {Seccion.Nombre}";
		public string Indicadores => string.IsNullOrEmpty(Seccion.Indicadores) ? string.Empty : $"INDICADORES : {Seccion.Indicadores}";
		public string Resultado => $"Puntaje: {Puntaje} . {Seccion.RangoValido?.Nombre}";

	}


}
