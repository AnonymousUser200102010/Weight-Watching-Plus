﻿using System;
using System.Linq;
using System.Windows.Forms;

namespace WeightWatchingProgramPlus
{
	public class ErrorHandler
	{
		
		readonly string[] title = {
			"Error: ValueIsNull",
			"You're overeating.",
			"You're overeating."
		};
		
		readonly MessageBoxButtons[] messageBoxButton = {
			MessageBoxButtons.OK,
			MessageBoxButtons.OK,
			MessageBoxButtons.YesNo
		};
		
		readonly MessageBoxIcon[] messageBoxIcon = {
			MessageBoxIcon.Error,
			MessageBoxIcon.Information,
			MessageBoxIcon.Warning
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


