using MyTimer.WPF.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyTimer.WPF
{
	public class ViewModelMain : ViewModelBase
	{

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

		private void StartTimer()
		{

		}


	}
}
