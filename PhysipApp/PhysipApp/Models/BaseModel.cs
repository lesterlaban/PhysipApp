using System;
using System.Collections.Generic;
using System.Text;

namespace PhysipApp.Models
{
	public class BaseModel
	{
		public bool Estado { get; set; }
		public string DescripcionEstado => Estado ? "Activo" : "Inactivo";
	}
}
