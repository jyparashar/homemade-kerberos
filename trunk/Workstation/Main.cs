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
			Key aliceKey = new Key("ABCDEFGH");
			
			#region Throw our server
			
			RemotingConfiguration.Configure(Application.ExecutablePath + ".config", false);	
			
			#endregion			
			
			#region Connection with server
			
			System.Configuration.AppSettingsReader configurationAppSettings =
				new System.Configuration.AppSettingsReader();
			String url = ((string)(configurationAppSettings.GetValue(
				"RemotingUrl", typeof(string))));
			
			RemotingConfiguration.Configure(Application.ExecutablePath + ".config", false);			
			IKdc kdc = (IKdc)Activator.GetObject(typeof(ShareClasses.IKdc), url);
			
			#endregion
			
			#region AS_REQ
			
			User alice = new User("Alice");
			KRB_AS_REQ asReq = new KRB_AS_REQ(alice);
			KRB_AS_REP asRep = kdc.AS(asReq);
			
			#endregion
			
			#region TGS_REQ
			
			User bob = new User("Bob");
			Authenticator auth = new Authenticator(aliceKey);
			KRB_TGS_REQ tgsReq = new KRB_TGS_REQ(asRep.GetTGT(aliceKey), auth, bob);
			KRB_TGS_REP tgsRep = kdc.TGS(tgsReq);
			
			#endregion
		}
	}
}