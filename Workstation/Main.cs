using System;
using System.Xml;
using ShareClasses;
using System.Runtime.Remoting;
using System.Windows.Forms;

namespace Workstation
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			System.Configuration.AppSettingsReader configurationAppSettings =
				new System.Configuration.AppSettingsReader();
			String url = ((string)(configurationAppSettings.GetValue(
				"RemotingUrl", typeof(string))));
			
			Console.WriteLine("KDC: " + url);
			
			RemotingConfiguration.Configure(Application.ExecutablePath + ".config", false);			
			IKDC kdc = (IKDC)Activator.GetObject(typeof(ShareClasses.IKDC), url);
			
			User alice = new User("Alice");
			SessionKey sk = null;
			TGT tgt = null;			
			kdc.AS(alice, out sk, out tgt);			
			Console.WriteLine(sk.test + " - " + tgt.test);
		}
	}
}