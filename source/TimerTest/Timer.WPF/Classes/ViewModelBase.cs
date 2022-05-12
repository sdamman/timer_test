using System;
using System.ComponentModel;
using System.Diagnostics;

namespace MyTimer.WPF.Classes
{
	public class ViewModelBase : INotifyPropertyChanged
	{


		public event PropertyChangedEventHandler PropertyChanged;

		public void NotifyPropertyChanged(string propertyName)
		{
			VerifyPropertyName(propertyName);
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		public void VerifyPropertyName(string propertyName)
		{
			if (TypeDescriptor.GetProperties(this)[propertyName] == null)
			{
				string msg = "Invalid property name:  " + propertyName;
				throw new Exception(msg);
			}
		}


	}
}
