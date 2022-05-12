using MyTimer.WPF.Classes;
using MyTimer.WPF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MyTimer.WPF
{
	public class ViewModelMain : ViewModelBase
	{
		TimerService tService = new TimerService();

		private string statusMessage = null;
		public string StatusMessage
		{
			get => statusMessage;
			set
			{
				if (value != statusMessage)
				{
					statusMessage = value;
					NotifyPropertyChanged(nameof(StatusMessage));
				}
			}
		}

		private readonly bool CanStartTimer = true;
		private RelayCommand commandStartTimer;
		public ICommand CommandStartTimer
		{
			get
			{
				if (commandStartTimer == null)
				{
					commandStartTimer = new RelayCommand(param => StartTimer(),
					param => CanStartTimer);
				}
				return commandStartTimer;
			}
		}

		private readonly bool CanEndTimer = true;
		private RelayCommand commandEndTimer;
		public ICommand CommandEndTimer
		{
			get
			{
				if (commandEndTimer == null)
				{
					commandEndTimer = new RelayCommand(param => EndTimer(),
					param => CanEndTimer);
				}
				return commandEndTimer;
			}
		}


		private void StartTimer()
		{
			//TestClass1 tc = new();
			//StatusMessage += "\ntesting";
			//StatusMessage = tc.GetSomethingBack(this);
			tService.StartTimer(this);
		}

		private void EndTimer()
		{
			CancellationTokenSource source = new();
			CancellationToken token = source.Token;
			tService.StopAsync(token);
		}



	}
}
