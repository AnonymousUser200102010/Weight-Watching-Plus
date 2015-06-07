#region Using Directives

using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using UniversalHandlersLibrary;

#endregion

namespace WeightWatchingProgramPlus
{

	/// <summary>
	/// Functions whose primary purpose is networking and internet operations, but who don't have a more pressing primary function.
	/// </summary>
	internal class NetworkOps : INetOps
	{
		
		private readonly IMainForm MainForm;

		private TcpListener server = new TcpListener (IPAddress.Any, IPEndPoint.MinPort);

		private delegate void SLDelegate (int portNum);

		public NetworkOps (IMainForm mainForm)
		{

			this.MainForm = mainForm;
			
		}

		public int ServerConnectionStatus
		{ 
			
			get { return OperationalPhase [0]; } 
			
			set
			{ 
				
				if (value == 0 && OperationalPhase [0] != 0)
				{
					
					DisconnectServerGracefully(0);
					
				}
				
				OperationalPhase [0] = value; 
			
				MainForm.SetSyncConnectionItems();
				
			}
			
		}

		private void DisconnectServerGracefully (int hashCode)
		{
			
			if (server.Server.IsBound && (server.GetHashCode() == hashCode || hashCode == 0))
			{
			
				server.Server.Close();
				
				server.Server.Dispose();
						
				server.Stop();
				
				ServerConnectionStatus = 0;
					
				#if DEBUG
								
				Console.WriteLine("Server successfully shutdown and will no longer accept requests.");
									
				#endif
				
			}
			
		}

		public int ClientConnectionStatus
		{ 
			
			get { return OperationalPhase [1]; } 
			
			set
			{
				
				OperationalPhase [1] = value;
			
				MainForm.SetSyncConnectionItems();
				
			}
		
		}

		/// <summary>
		/// Operational phase of [0] = server/[1] = client.
		/// </summary>
		/// <example>
		/// 0 = Disconnected.
		/// 1 = Initializing.
		/// 2 = Operational and Awaiting.
		/// 3 = Operational and Transmitting/Recieving.
		/// </example>
		private volatile int[] OperationalPhase = {
			0,
			0
		};

		public void StartListen (int port)
		{
			
			if (!server.Server.IsBound && MainForm.SyncEnabled)
			{
			
				var tempDelegate = new SLDelegate (StartListenAsync);
				
				tempDelegate.BeginInvoke(port, null, null);
			
			}
			
		}

		private async void StartListenAsync (int port)
		{
			
			ServerConnectionStatus = 1;
			
			server = new TcpListener (IPAddress.Any, port);
			
			int serverCurrentHashCode = 0;
				
			server.Server.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.AcceptConnection, 1);
					
			server.Start();
			
			serverCurrentHashCode = server.GetHashCode();
				
			#if DEBUG
					
			Console.WriteLine("Program is listening for connections. (Server listen successful)");
					
			#endif
			
			while (true)
			{
				
				if (!server.Server.IsBound)
				{
					
					break;
					
				}
					
				ServerConnectionStatus = 2;
				
				#if DEBUG
					
				Console.WriteLine("Program is waiting for connection attempts.");
						
				#endif
				
				try
				{
					
					while (!server.Pending())
					{
						
						Thread.Sleep(1000);
				         
					}
					
				}
				catch (InvalidOperationException)
				{
				}
				
				if (MainForm.SyncEnabled && server.Server.IsBound)
				{
					
					using (TcpClient client = server.AcceptTcpClient())
					{
						
						ServerConnectionStatus = 3;
						
						#if DEBUG
							
						Console.WriteLine(string.Format("You ({0}) have successfully accepted a connection from {1}", server.Server.LocalEndPoint, client.Client.LocalEndPoint));
									
						#endif
							
						string message = null;
						
						string directory = null;
								
						string fileName = null;
						
						using (StreamReader stream = new StreamReader (client.GetStream()))
						{
							
							message = stream.ReadLineAsync().Result;
							
							directory = stream.ReadLineAsync().Result;
							
							fileName = stream.ReadLineAsync().Result;
							
							if (message.Contains("send", StringComparison.OrdinalIgnoreCase))
							{
									
								await RecieveOps(client, message, directory, fileName);
									
							}
							else if (message.Contains("file", StringComparison.OrdinalIgnoreCase))
							{
									
								StartSend(MainForm.SyncIPAddress, int.Parse(MainForm.SyncSendPort, CultureInfo.InvariantCulture), "send file", directory, fileName, string.Format(CultureInfo.InvariantCulture, "{0}\\{1}", directory, fileName));
												
							}
							else if (message.Contains("uptime", StringComparison.OrdinalIgnoreCase))
							{
											
								StartSend(MainForm.SyncIPAddress, int.Parse(MainForm.SyncSendPort, CultureInfo.InvariantCulture), "send time", GlobalVariables.StartTime.ToString(), null, null);
											
							}
							
						}
						
						client.GetStream().Close();
						
					}
					
					#if DEBUG
						
					Console.WriteLine("Client tasks successfully handled.");
									
					#endif
					
				}
				else
				{
						
					break;
						
				}
						
			}
			
			#if DEBUG
						
			Console.WriteLine("Server finalizing shutdown....");
							
			#endif
			
			DisconnectServerGracefully(serverCurrentHashCode);
			
			#if DEBUG
						
			Console.WriteLine("Server shut down successfully.");
							
			#endif
				
			Thread.CurrentThread.Abort();
			
		}

		public async void StartSend (string ipAddress, int port, string message, string additionalInfo, string fileName, string pathToFile)
		{
			
			ClientConnectionStatus = 1;
			
			IPAddress[] tempIPAddress = Dns.GetHostAddresses(ipAddress);
			
			IPEndPoint ipEnd = new IPEndPoint (tempIPAddress [0], port);
					
			TcpClient client = new TcpClient (ipEnd);
					
			client.Connect(ipAddress, int.Parse(MainForm.SyncListenPort));
					
			#if DEBUG
					
			Console.WriteLine("Program successfully connected to server. (Client connection successful)");
					
			#endif
			
			ClientConnectionStatus = 2;
			
			await SendOps(client, message, additionalInfo, fileName, pathToFile);
			
			Thread.CurrentThread.Abort();
			
			
		}

		private async Task SendOps (TcpClient client, string message, string additionalInfo, string fileName, string pathToFile)
		{
			
			ClientConnectionStatus = 3;
			
			StreamWriter messageStream = new StreamWriter (client.GetStream());
				
			await messageStream.WriteAsync(string.Format("{0}\n", message));
			
			await messageStream.WriteAsync(string.Format("{0}\n", additionalInfo));
			
			await messageStream.WriteAsync(string.Format("{0}\n", fileName));
				
			await messageStream.FlushAsync();
			
			if (!message.Contains("request", StringComparison.InvariantCultureIgnoreCase) && !message.Contains("receive", StringComparison.InvariantCultureIgnoreCase))
			{
				
				MainForm.MainFormState(true);
			
				byte[] buffer = new byte[1];
				
				using (Stream fileStream = File.OpenRead(pathToFile))
				{
						
					// Alocate memory space for the file
					buffer = new byte[fileStream.Length];
						
					await fileStream.ReadAsync(buffer, 0, (int)fileStream.Length);
						
				}
				
				// Open a TCP/IP Connection and send the data
				using (NetworkStream networkStream = client.GetStream())
				{
					
					await networkStream.WriteAsync(buffer, 0, buffer.GetLength(0));
					
				}
				
			}
			
			messageStream.Close();
			
			messageStream.Dispose();
			
			#if DEBUG
			
			Console.WriteLine("File or message successfully transmitted. (Send operation successful)");
				
			#endif
				
			MainForm.MainFormState(false);
			
			Thread.CurrentThread.Abort();
			
		}

		private async Task RecieveOps (TcpClient client, string message, string additionalInfo, string fileName)
		{
			
			if (message.Contains("file", StringComparison.OrdinalIgnoreCase))
			{
				
				MainForm.MainFormState(true);
			
				string filePath = string.Format("{0}\\{1}\\{2}", AppDomain.CurrentDomain.BaseDirectory, additionalInfo, fileName);
				
				if (File.Exists(filePath))
				{
					
					File.Delete(filePath);
					
				}
				
				using (var networkStream = client.GetStream())
				{
					
					using (Stream fileStream = File.OpenWrite(filePath))
					{
							
						byte[] buffer = new byte[8192];
						
						int bytesRead;
				        
						while ((bytesRead = await networkStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
						{
				        	
							await fileStream.WriteAsync(buffer, 0, bytesRead);
				        	
						}
							
					}
					
				}
				
				#if DEBUG
				
				Console.WriteLine("File successfully written. (File retrieval successful)");
				
				#endif
				
			}
			else if (message.Contains("uptime", StringComparison.OrdinalIgnoreCase) && !GlobalVariables.ClientStateOverride)
			{
				
				GlobalVariables.IsClient = DateTime.Compare(DateTime.Parse(additionalInfo), GlobalVariables.StartTime) < 0;
							
			}
				
			MainForm.MainFormState(false);
			
			Thread.CurrentThread.Abort();
			
		}
		
	}
	
}


