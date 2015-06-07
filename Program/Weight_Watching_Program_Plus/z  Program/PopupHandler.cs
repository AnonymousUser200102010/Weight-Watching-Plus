using System;
using System.Linq;
using System.Windows.Forms;

namespace WeightWatchingProgramPlus
{

	internal class PopupHandler : IPopup
	{
		
		private readonly string[] Title = {
			"Error: Value Is Null",
			"You're overeating",
			"You're overeating",
			"Midnight Snacking Penalty",
			"Error: Value Invalid",
			"Confirm Deletion",
			"Calorie Reset Error",
			"Copyright Information",
			"Search Term Not Found",
			"Error: Crashed Recently"
		};
		
		public DialogResult CreatePopup (string message, int popupCode)
		{
			
			return CreatePopup(message, popupCode, false, null);
			
		}
		
		public DialogResult CreatePopup (string message, int popupCode, Control controlItem)
		{
			
			return CreatePopup(message, popupCode, true, controlItem);
			
		}

		#region Create Popup Summary
		/// <summary>
		/// Handles the creation of popup messageboxes.
		/// </summary>
		/// <param name="message">
		/// The text that goes in the body of the messagebox.
		/// </param>
		/// <param name="popupCode">
		/// The code that defines how the popup will set itself up.
		/// </param>
		/// <param name="hasAdditionalActions">
		/// Do you need to select the control item after?
		/// </param>
		/// <param name="controlItem">
		/// The items to be selected if additional actions are required.
		/// </param>
		/// <returns>
		/// Returns a DialogResult variable based on the actions taken by the user with regards to the popup created herein.
		/// </returns>
		
		#endregion
		private DialogResult CreatePopup (string message, int popupCode, bool hasAdditionalActions, Control controlItem)
		{
			
			MessageBoxButtons messageBoxButton = MessageBoxButtons.OK;
			
			MessageBoxIcon messageBoxIcon = MessageBoxIcon.Information;
			
			switch (popupCode)
			{
					
				case 0:
				case 4:
					messageBoxIcon = MessageBoxIcon.Error;
					break;
					
				case 1:
					messageBoxIcon = MessageBoxIcon.Hand;
					break;
					
				case 2:
				case 5:
					messageBoxButton = MessageBoxButtons.YesNo;
					messageBoxIcon = MessageBoxIcon.Question;
					break;
					
				case 3:
					messageBoxButton = MessageBoxButtons.OKCancel;
					break;
					
				case 6:
					messageBoxIcon = MessageBoxIcon.Warning;
					break;
					
				case 8:
					messageBoxIcon = MessageBoxIcon.Exclamation;
					break;
					
				case 9:
					messageBoxIcon = MessageBoxIcon.Error;
					break;
					
			}
			
			DialogResult dialogResult = MessageBox.Show(message, this.Title [popupCode], messageBoxButton, messageBoxIcon, MessageBoxDefaultButton.Button1);
			
			if (hasAdditionalActions)
			{
				
				controlItem.Select();
				
			}
			
			return dialogResult;
		}
	}
}


