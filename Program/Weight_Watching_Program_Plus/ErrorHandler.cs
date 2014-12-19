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
		
		readonly MessageBoxButtons[] messageBoxButton = {
			MessageBoxButtons.OK,
			MessageBoxButtons.OK
		};
		
		readonly MessageBoxIcon[] messageBoxIcon = {
			MessageBoxIcon.Error,
			MessageBoxIcon.Information
		};
		
		public  void ErrorMessageBox (string message, Control controlItem, int errorCode, bool hasAdditionalActions)
		{
			DialogResult dialogResult = MessageBox.Show (message, this.title [errorCode], this.messageBoxButton [errorCode], this.messageBoxIcon [errorCode], MessageBoxDefaultButton.Button1);
			if(hasAdditionalActions){
				controlItem.Select();
			}
		}
	}
}


