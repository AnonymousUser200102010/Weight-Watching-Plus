using System;
using System.Linq;
using System.Windows.Forms;

namespace WeightWatchingProgramPlus
{
	public class PopupHandler
	{
		
		readonly string[] title = {
			"Error: ValueIsNull",
			"You're overeating.",
			"You're overeating.",
			"Midnight Snacking Penalty.",
			"Error: ValueIsIdentical"
		};
		
		readonly MessageBoxButtons[] messageBoxButton = {
			MessageBoxButtons.OK,
			MessageBoxButtons.OK,
			MessageBoxButtons.YesNo,
			MessageBoxButtons.OKCancel,
			MessageBoxButtons.OK
		};
		
		readonly MessageBoxIcon[] messageBoxIcon = {
			MessageBoxIcon.Error,
			MessageBoxIcon.Information,
			MessageBoxIcon.Warning,
			MessageBoxIcon.Information,
			MessageBoxIcon.Error
		};
		
		public DialogResult ErrorMessageBox (string message, Control controlItem, int errorCode, bool hasAdditionalActions)
		{
			DialogResult dialogResult = MessageBox.Show (message, this.title [errorCode], this.messageBoxButton [errorCode], this.messageBoxIcon [errorCode], MessageBoxDefaultButton.Button1);
			if(hasAdditionalActions){
				controlItem.Select();
			}
			return dialogResult;
		}
	}
}


