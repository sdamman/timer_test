using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyTimer.WPF.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MyTimer.WPF
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private ViewModelMain vmm;

		public static IHost Host { get; private set; }

		public App()
		{
			Host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
					.ConfigureServices((context, services) =>
					{
						ConfigureServices(services);
					})
					.Build();
		}


		private void ConfigureServices(IServiceCollection services)
		{
			// Add Services
			services.AddHostedService<TimerService>();
			services.AddSingleton<wndMain>();
		}

		protected override async void OnExit(ExitEventArgs e)
		{
			await Host!.StopAsync();
			base.OnExit(e);
		}

		protected override async void OnStartup(StartupEventArgs e)
		{
			await Host!.StartAsync();

			var wnd = Host.Services.GetRequiredService<wndMain>();

			vmm = new ViewModelMain();
			wnd.DataContext = vmm;
			wnd.Show();

			base.OnStartup(e);
		}
	}
}
