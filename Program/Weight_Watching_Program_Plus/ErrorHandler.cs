using System;
using System.Linq;
using System.Windows.Forms;

namespace WeightWatchingProgramPlus
{
	public class ErrorHandler
	{
		 readonly string[] title = {
			"Error: ValueIsNull",
			"You're overeating."
		};

		public  string[] Title
		{
			get
			{
				return title;
			}
		}

		 readonly MessageBoxButtons[] messageBoxButton = {
			MessageBoxButtons.OK,
			MessageBoxButtons.OK
		};

		public  MessageBoxButtons[] MessageBoxButton
		{
			get
			{
				return messageBoxButton;
			}
		}

		public  void errorMessageBox (string message, Control controlItem, int errorCode, bool hasAdditionalActions)
		{
			MessageBoxIcon[] messageBoxIcon = {
				MessageBoxIcon.Error,
				MessageBoxIcon.Information
			};
			DialogResult dialogResult = MessageBox.Show (message, title [errorCode], messageBoxButton [errorCode], messageBoxIcon [errorCode]);
			switch (hasAdditionalActions)
			{
				default:
					controlItem.Select ();
					break;
			}
		}
	}
}


