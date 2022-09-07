using PhysipApp.ViewModels;
using PhysipApp.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace PhysipApp
{
	public partial class AppShell : Xamarin.Forms.Shell
	{
		public AppShell()
		{
			InitializeComponent();
			Routing.RegisterRoute(nameof(NewZonaPage), typeof(NewZonaPage));
			Routing.RegisterRoute(nameof(NewNivelPage), typeof(NewNivelPage));
			Routing.RegisterRoute(nameof(NewPreguntaPage), typeof(NewPreguntaPage));
			Routing.RegisterRoute(nameof(NewRecursoPage), typeof(NewRecursoPage));
			Routing.RegisterRoute(nameof(RecursoDetailPage), typeof(RecursoDetailPage));
			Routing.RegisterRoute(nameof(NewTratamientoPage), typeof(NewTratamientoPage));
			Routing.RegisterRoute(nameof(TratamientoDetailPage), typeof(TratamientoDetailPage));
			Routing.RegisterRoute(nameof(PreguntaDetailPage), typeof(PreguntaDetailPage));
		}

	}
}
