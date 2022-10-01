using PhysipApp.ViewModels;
using PhysipApp.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace PhysipApp
{
	public partial class AppShellPaciente : Xamarin.Forms.Shell
	{
		public AppShellPaciente()
		{
			InitializeComponent();
			Routing.RegisterRoute(nameof(NewConsultaPage), typeof(NewConsultaPage));
			Routing.RegisterRoute(nameof(ConsultaDetailPage), typeof(ConsultaDetailPage));
			Routing.RegisterRoute(nameof(ConsultaRecursoPage), typeof(ConsultaRecursoPage));
			
		}

	}
}
