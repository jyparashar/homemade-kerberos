using System;
using System.Windows.Forms;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;

namespace KDC
{
	class MainClass
	{
		public static void Main(string[] args)
		{					
			RemotingConfiguration.Configure(Application.ExecutablePath + ".config", false);	
			
			Console.WriteLine("KDC Started.");
			Console.ReadLine();
		}
	}
}