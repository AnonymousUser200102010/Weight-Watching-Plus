using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace UniversalHandlersLibrary
{
	
	/// <summary>
	/// Errors handler entry point.
	/// </summary>
	public static class Errors
	{
		#region Handler & Related Functions
		
			#region Writer Summary
			/// <summary>
			/// Writes to the Error.dmp file. Should only be used ONLY if just a writing operation should be initiated. It is also not documented well enough for regular use, so it's advised only advanced users use this outside the library's code.
			/// </summary>
			/// <param name="providedException">
			/// The exception that will be handled while writing to the Error.dmp file.
			/// </param>
			/// <param name="prune">
			/// Is this operation checking the file to see if it should be pruned?
			/// </param>
			/// <param name="pruneCutoff">
			/// The file size at which to prune the Error.dmp file. (default is 524288)
			/// </param>
			
			#endregion
			public static void Writer(Exception providedException, bool prune, int pruneCutoff)
			{	
				
				string startingMessage = string.Format(CultureInfo.InvariantCulture, "{0}:\nException Message:\n{1}\n\nException Stack Trace:\n{2}\n\n{3}", DateTime.Now.ToString("MMMM dd hh:mm:ss tt", CultureInfo.InvariantCulture), providedException.Message, providedException.StackTrace, providedException.InnerException != null ? string.Format(CultureInfo.InvariantCulture, "Inner Exception Message:\n{0}\n\nInner Exception Stack Trace:\n{1}\n\n", providedException.InnerException.Message, providedException.InnerException.StackTrace) : null);
				
				InternalFunctions.Writer(startingMessage, InternalFunctions.ReturnFilePath("Error.dmp"), prune, pruneCutoff);
				
			}
			
			#region Handler Summary
			/// <summary>
			/// Error handler.
			/// </summary>
			/// <param name="providedException">
			/// The exception that will be handled.
			/// </param>
			/// <param name="write">
			/// Is this a write operation?
			/// </param>
			/// <param name="prune">
			/// Is this operation also checking the file to see if it should be pruned?
			/// </param>
			/// <param name="pruneCutoff">
			/// The file size at which to prune the Error.dmp file. (default is 524288)
			/// </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			/// Thrown if the pruneCutOff value is less than 102400, or greater than 1000000.
			/// </exception>
			
			#endregion
			public static void Handler (Exception providedException, bool write, bool prune, int pruneCutoff)
			{
				
				if (pruneCutoff < 102400 || pruneCutoff > 1000000)
				{
						
					Errors.Handler(new ArgumentOutOfRangeException("pruneCutoff", pruneCutoff,"UniversalHandlersLibrary: prune cutoff must be between 102400 and 1000000"), true, true, 524288);
						
				}
				
				if(write)
				{
					
					Writer(providedException, prune, pruneCutoff);
					
				}
				else if(prune)
				{
					string errorDmp = InternalFunctions.ReturnFilePath("Error.dmp");
					
					InternalFunctions.PruneCheck(errorDmp, pruneCutoff);
					
				}
				
				throw providedException;
				
			}
			
		#endregion
		
		#region Premade Exceptions
		/// <summary>
		/// A list of premade exceptions for convenience.
		/// </summary>
		/// <param name="offendingCode">
		/// Where did the exception originate from?
		/// </param>
		/// <param name="areaWhereOffendingCodeResides">
		/// Where within the originating code did the exception occur?
		/// </param>
		/// <param name="exceptionNumber">
		/// The number of the exception you'd like to use.
		/// </param>
		/// <returns>
		/// For exception number: 0, a FormatException with the text "Argument failed to produce a valid result." will be produced.
		/// </returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		/// Thrown if the exceptionNumber is less than zero or greater than the current number of premade exceptions.
		/// </exception>
		
		#endregion
		public static Exception PremadeExceptions(string offendingCode, string areaWhereOffendingCodeResides, int exceptionNumber)
		{
			const int lower = 0;
			const int upper = 0;
			//Upper is 0 until a new exception is created.
			
			if (exceptionNumber < lower || exceptionNumber > upper)
			{
				
				Errors.Handler(new ArgumentOutOfRangeException("exceptionNumber", exceptionNumber, "Value must be between " + lower + " and " + upper), true, true, 524288);
				
			}
			
			Exception e = null;
			
			switch(exceptionNumber)
			{
					
				case 0:
					e = new FormatException (string.Format(CultureInfo.InvariantCulture, "{0}: {1}: {2}", offendingCode, areaWhereOffendingCodeResides, "Argument failed to produce a valid result."));
					break;
					
			}
			
			return e;
			
		}
		
	}
	
	/// <summary>
	/// Messages handler entry point.
	/// </summary>
	public static class Messages
	{
		
		#region Handler
		/// <summary>
		/// Message handler
		/// </summary>
		/// <param name="message">
		/// The message to be handled.
		/// </param>
		/// <param name="nameOfAppWithoutExtension">
		/// The application's name, which will be printed before the message.
		/// </param>
		/// <param name="prune">
		/// Is this operation checking the file to see if it should be pruned?
		/// </param>
		/// <param name="pruneCutoff">
		/// The file size at which to prune the Messages.txt file. (default is 102400)
		/// </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		/// Thrown if the pruneCutOff value is less than 102400, or greater than 1000000.
		/// </exception>
		
		#endregion
		public static void Handler (string message, string nameOfAppWithoutExtension, bool prune, int pruneCutoff)
		{
			
			try
			{
				
				if (pruneCutoff < 102400 || pruneCutoff > 1000000)
				{
						
					throw new ArgumentOutOfRangeException("pruneCutoff", pruneCutoff,"UniversalHandlersLibrary: prune cutoff must be between 102400 and 1000000");
						
				}
				
				string startingMessage = string.Format(CultureInfo.InvariantCulture, "{0}: {1} Time: {2}\n\n", nameOfAppWithoutExtension, message, DateTime.Now.ToString("MM-dd-yy hh:mm tt", CultureInfo.InvariantCulture));
				
				InternalFunctions.Writer(startingMessage, InternalFunctions.ReturnFilePath("Messages.txt"), prune, pruneCutoff);
				
			}
			catch (Exception e)
			{
				
				Errors.Handler(e, true, true, 524288);
				
			}
			
		}
		
	}
	
	/// <summary>
	/// Backend related functions handler entry point.
	/// </summary>
	public static class BackEnd
	{
		
		#region Setup Console Summary
		/// <summary>
		/// Sets up the console. If debugging, the console shows. Otherwise, it does nothing.
		/// </summary>
		/// <param name="debug">
		/// Is the parent program debugging?
		/// </param>
		
		#endregion
		public static void SetupConsole(bool debug)
		{
			
			if(debug)
			{
				
				SafeNativeMethods.AllocConsole();	
				
			}
			
		}
		
	}
	
	/// <summary>
	/// All publicly reachable reading and writing related functions handler entry point.
	/// </summary>
	public static class IOHandler
	{
		#region Mass Delete Same Name Files Overrides
			
			#region Delete Same Name Files No Bools Override Summary
			/// <summary>
			/// Checks all files in all subdirectories of the root directory against the file provided to see if their names match. If they do, it deletes the appropriate file.
			/// </summary>
			/// <param name="fileToSearchWith">
			/// The file, or logically it's information, that you'll be searching with.
			/// </param>
			/// <param name="rootDirectory">
			/// The top of the directory hierarchy you'd like to search in.
			/// </param>
			
			#endregion
			public static void DeleteAllSameNamedFiles(FileInfo fileToSearchWith, string rootDirectory)
			{
				
				DeleteAllSameNamedFiles(fileToSearchWith, rootDirectory, false, true);
				
			}
			
			#region Delete Same Name Files Ignore Case Bool Only Override Summary
			/// <summary>
			/// Checks all files in all subdirectories of the root directory against the file provided to see if their names match. If they do, it deletes the appropriate file.
			/// </summary>
			/// <param name="fileToSearchWith">
			/// The file, or logically it's information, that you'll be searching with. This is the root file.
			/// </param>
			/// <param name="rootDirectory">
			/// The top of the directory hierarchy you'd like to search in.
			/// </param>
			/// <param name="ignoreCase">
			/// If true, the function will ignore casing when checking the files in the hierarchy against the root file.
			/// </param>
			
			#endregion
			public static void DeleteAllSameNamedFiles(FileInfo fileToSearchWith, string rootDirectory, bool ignoreCase)
			{
				
				DeleteAllSameNamedFiles(fileToSearchWith, rootDirectory, ignoreCase, true);
				
			}
		
		#endregion
		
		#region Delete Same Name Files Summary
		/// <summary>
		/// Checks all files in all subdirectories of the root directory against the file provided to see if their names match. If they do, it deletes the appropriate file.
		/// </summary>
		/// <param name="fileToSearchWith">
		/// The file, or logically it's information, that you'll be searching with. This is the root file.
		/// </param>
		/// <param name="rootDirectory">
		/// The top of the directory hierarchy you'd like to search in.
		/// </param>
		/// <param name="ignoreCase">
		/// If true, the function will ignore casing when checking the files in the hierarchy against the root file.
		/// </param>
		/// /// <param name="deleteAtDirectory">
		/// If true, the function will delete the provided file once a file is found in the directory hierarchy with the exact same filename. If false, deletes the file in the hierarchy, and preserves the root file.
		/// </param>
		
		#endregion
		public static void DeleteAllSameNamedFiles(FileInfo fileToSearchWith, string rootDirectory, bool ignoreCase, bool deleteAtDirectory)
		{

			Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Starting deletion...Looking in '{0}' for files with the exact same name as '{1}', while {2} ignoring case.", rootDirectory, fileToSearchWith.Name, ignoreCase ? "also" : "not"));
			
			bool stopChecking = false;
			
			for (int currentDirectory = 0, maxLengthDir = Directory.GetDirectories(rootDirectory).Length; currentDirectory < maxLengthDir; currentDirectory++)
			{
				
				string d = Directory.GetDirectories(rootDirectory)[currentDirectory];
				
				Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Now looking in {0}", d));
				
				if(!stopChecking)
				{
					
					if(!fileToSearchWith.DirectoryName.Equals(d))
					{
						
						DirectoryInfo dI = new DirectoryInfo(d);
					
						for (int currentFile = 0, maxLength = dI.GetFiles().Length; currentFile < maxLength; currentFile++)
						{
							
							FileInfo file = dI.GetFiles()[currentFile];
							
							if (file != null && ignoreCase ? file.Name.Equals(fileToSearchWith.Name, StringComparison.OrdinalIgnoreCase) : file.Name.Equals(fileToSearchWith.Name))
							{
								
								Console.WriteLine(string.Format(CultureInfo.InvariantCulture, deleteAtDirectory ? "{0}\\{1} is being deleted, {2}\\{3} will be preserved." : "{2}\\{3} is being deleted, {0}\\{1} will be preserved.", fileToSearchWith.DirectoryName, fileToSearchWith.Name, file.DirectoryName, file.Name));
								
								if (deleteAtDirectory)
								{
									
									fileToSearchWith.Delete();
									
								}
								else
								{
									
									file.Delete();
									
								}
								
								stopChecking = true;
								
								return;
							}
						}
						
					}
					else
					{
						
						Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Cannot search {0} as it is the directory of the file you're searching with.", d));
						
					}
					
				}
				
			}
		}

		#region Create Folder Tree Override Summary
		/// <summary>
		/// Creates a folder hierarchy of your choosing using the directory hierarchy provided.
		/// </summary>
		/// <param name="directoryHierarchy">
		/// An array of strings with the FULL directory path of the folder you'd like to create.
		/// </param>
		/// <example>
		/// "SomeDrive\\SomeRoot\\Folder1\\" is a correct value for the array. You can also use "SomeFolder\\SomeSubFolder\\" as well to indicate the root directory is the application's root directory. "SomeDrive\\SomeRoot\\Folder1" is not correct.
		/// </example>
		
		#endregion
		public static void CreateFolderTree(string[] directoryHierarchy)
		{
			
			CreateFolderTree(directoryHierarchy, null, true, true);
			
		}
		
		#region Create Folder Tree Summary
		/// <summary>
		/// Creates a folder hierarchy of your choosing using the directory hierarchy provided.
		/// </summary>
		/// <param name="directoryHierarchy">
		/// An array of strings with the FULL directory path of the folder you'd like to create.
		/// </param>
		/// <example>
		/// "SomeDrive\\SomeRoot\\Folder1\\" is a correct value for the array. You can also use "SomeFolder\\SomeSubFolder\\" as well to indicate the root directory is the application's root directory. "SomeDrive\\SomeRoot\\Folder1" is not correct.
		/// </example>
		/// <param name="appendedDirectoryRoot">
		/// The directory root to be applied to all folders that are created when their related bool values are true. In other words, it does not apply to any folders in the directoryHierarchy array.
		/// </param>
		/// <example>
		/// If 'createFilesFolder' is true, the program would attempt to make a folder at 'appendedDirectoryRoot\\Files\\'. Else it would attempt to make a folder at 'Files\\'.
		/// </example>
		/// <param name="createFilesFolder">
		/// If true the 'Files\\' folder will be marked for creation, else not. This would remove the need to add it to the directoryHeirarchy array as it is a vital folder.
		/// </param>
		/// <param name="createTextFolder">
		/// If true the 'Files\\Text\\' folder will be marked for creation, else not. This would remove the need to add it to the directoryHeirarchy array as it is a vital folder.
		/// </param>
		
		#endregion
		public static void CreateFolderTree(string[] directoryHierarchy, string appendedDirectoryRoot, bool createFilesFolder, bool createTextFolder)
		{
			
			if(createFilesFolder && !Directory.Exists(string.IsNullOrEmpty(appendedDirectoryRoot) ? "Files\\" : string.Format(CultureInfo.InvariantCulture, "{0}\\Files\\", appendedDirectoryRoot)))
			{
				
				Directory.CreateDirectory(string.IsNullOrEmpty(appendedDirectoryRoot) ? "Files\\" : string.Format(CultureInfo.InvariantCulture, "{0}\\Files\\", appendedDirectoryRoot));
				
			}
			
			if(createTextFolder && !Directory.Exists(string.IsNullOrEmpty(appendedDirectoryRoot) ? "Files\\Text\\" : string.Format(CultureInfo.InvariantCulture, "{0}\\Files\\Text\\", appendedDirectoryRoot)))
			{
				
				Directory.CreateDirectory(string.IsNullOrEmpty(appendedDirectoryRoot) ? "Files\\Text\\" : string.Format(CultureInfo.InvariantCulture, "{0}\\Files\\Text\\", appendedDirectoryRoot));
				
			}
			
			if(directoryHierarchy != null)
			{
				
				for (int curDir = 0, directoryHierarchyLength = directoryHierarchy.Length; curDir < directoryHierarchyLength; curDir++)
				{
					
					if (!Directory.Exists(directoryHierarchy[curDir]))
					{
						
						Directory.CreateDirectory(directoryHierarchy[curDir]);
						
					}
					
				}
				
			}
			
			//Currently there is little support for custom file trees in this program, so it's up to you to code for any folders beside "Files\\" and "Text\\".
			
		}
		
	}
	
	/// <summary>
	/// Dunno, FxCop told me to call it this. 'Magine there's a good reason.
	/// </summary>
	internal static class SafeNativeMethods
	{
		
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool AllocConsole ();
		
	}
	
	/// <summary>
	/// Global Functions
	/// </summary>
	public static class UniversalHandlersLibraryGlobal
	{
		
		#region Contains Override Summary
		/// <summary>
		/// Returns a value indicating if the provided String object occurs within the string.
		/// </summary>
		/// <param name="source">
		/// The string to check.
		/// </param>
		/// <param name="toCheck">
		/// The keyword to check for.
		/// </param>
		/// <param name="comp">
		/// The string comparison mode you wish to use for this operation.
		/// </param>
		/// <returns>
		/// true if the toCheck parameter occurs within the string, or if the toCheck value is an empty string (""); else false.
		/// </returns>
		
		#endregion
		public static bool Contains (this string source, string toCheck, StringComparison comp)
		{
			return source.IndexOf (toCheck, comp) >= 0;
			
		}
		
	}
	
	
	/// <summary>
	/// DO NOT USE ANY OF THESE FUNCTIONS OUTSIDE THE UNIVERSALHANDLERSLIBRARY MAIN CODE.
	/// </summary>
	internal static class InternalFunctions
	{
		
		#region Internal Writer Summary
		/// <summary>
		/// Central writing code for most writing operations required by this library.
		/// </summary>
		/// <param name="message">
		/// Message to write to the file.
		/// </param>
		/// <param name="file">
		/// The file to write to.
		/// </param>
		/// <param name="prune">
		/// Is this operation checking the file to see if it should be pruned?
		/// </param>
		/// <param name="pruneCutoff">
		/// The file size at which to prune the file.
		/// </param>
		
		#endregion
		internal static void Writer(string message, string file, bool prune, int pruneCutoff)
		{
			
			string temp = string.Format(CultureInfo.InvariantCulture, "{0}{1}", message, InternalFunctions.ReadFile(file));
			
			if(prune)
			{
					
				InternalFunctions.PruneCheck(file, pruneCutoff);
				
			}
				
			File.WriteAllText (file, temp);
			
			if(prune)
			{
					
				InternalFunctions.PruneCheck(file, pruneCutoff);
				
			}
			
		}
		
		#region Internal Reader Summary
		/// <summary>
		/// Central reading code for most reading operations required by the program.
		/// </summary>
		/// <param name="file">
		/// The file to read from.
		/// </param>
		/// <returns>
		/// Returns a string containing a parsed version of the file's contents.
		/// </returns>
		
		#endregion
		internal static string ReadFile(string file)
		{
			
			string returnString = null;
			
			if (File.Exists(file))
			{
				
				using (StreamReader sr = new StreamReader(file))
				{
					String line;
							
					while ((line = sr.ReadLine()) != null)
					{
								
						returnString += !string.IsNullOrWhiteSpace(line) ? string.Format(CultureInfo.InvariantCulture, "{0}\n", line) : "\n";
								
					}
							
					sr.Close();
				}
				
				return returnString;
				
			}
				
			return null;
			
		}
		
		#region Return File Path Summary
		/// <summary>
		/// Returns the most logical path available for the provided file.
		/// </summary>
		/// <param name="fileName">
		/// The name of the file.
		/// </param>
		/// <returns>
		/// Returns the desired file path of the provided file name.
		/// </returns>
		
		#endregion
		internal static string ReturnFilePath(string fileName)
		{
			
			string[] ImportantFileExtentions = {
				".dmp"
			};
			
			if (Directory.Exists("Files\\Text\\") && !ImportantFileExtentions.Any(s => fileName.Contains(s, StringComparison.OrdinalIgnoreCase)))
			{
				
				return string.Format(CultureInfo.InvariantCulture, "Files\\Text\\{0}", fileName);
					
			}
			
			if (Directory.Exists("Files\\"))
			{
				
				return string.Format(CultureInfo.InvariantCulture, "Files\\{0}", fileName);
				
			}
			
			return fileName;
		}
		
		#region Prune File Check Summary
		/// <summary>
		/// Checks to see if the provided file should be pruned.
		/// </summary>
		/// <param name="fileToCheck">
		/// File whose size is to be checked in this operation.
		/// </param>
		/// <param name="pruneCutoff">
		/// The size at which a file is pruned.
		/// </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		/// Thrown if the pruneCutOff value is less than 102400, or greater than 1000000.
		/// </exception>
		
		#endregion
		internal static void PruneCheck(string fileToCheck, int pruneCutoff)
		{
			
			if(File.Exists(fileToCheck))
			{
				
				long fileSize = new FileInfo(fileToCheck).Length;
				
				if (pruneCutoff < 102400 || pruneCutoff > 1000000)
				{
					
					throw new ArgumentOutOfRangeException("pruneCutoff", pruneCutoff,"UniversalHandlersLibrary: pruneCheck: prune cutoff must be between 102400 and 1000000");
					
				}
				
				if (fileSize < pruneCutoff)
				{
					
					return;
					
				}
				
				//102400 regular pruneCutoff Messages
				//524288 regular pruneCutoff Errors
				
				if (File.Exists(fileToCheck + ".old"))
				{
					
					File.Delete(fileToCheck + ".old");
					
				}
				
				File.Copy(fileToCheck, fileToCheck + ".old");
				
				File.Delete(fileToCheck);
				
				File.WriteAllText(fileToCheck, null);
				
			}
			
		}
		
	}
	
}