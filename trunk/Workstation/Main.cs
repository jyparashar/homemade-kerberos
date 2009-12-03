using System;
using System.Xml;
using ShareClasses;
using System.Runtime.Remoting;
using System.Windows.Forms;
using System.Configuration;

namespace Workstation
{
	class MainClass
	{
		public static void Main(string[] args)
		{	
			// I have to check the args
			string userName = args[0];
			string userRemo = args[1];
			
			Key aliceKey = new Key("ABCDEFGH");			
			
			#region Throw our server
			
			string confFile = Application.ExecutablePath + "." + userName.ToLower() + ".config";
			Console.WriteLine(confFile);
			RemotingConfiguration.Configure(confFile , false);	
			
			#endregion			
			
			#region Connection with server
			
			System.Configuration.AppSettingsReader configurationAppSettings =
				new System.Configuration.AppSettingsReader();
			//String url = (string)ConfigurationSettings.AppSettings["RemotingUrl"];
			String url = 
			IKdc kdc = (IKdc)Activator.GetObject(typeof(ShareClasses.IKdc), url);
			
			#endregion
			
			#region AS_REQ
			
			User alice = new User(userName);
			KRB_AS_REQ asReq = new KRB_AS_REQ(alice);
			KRB_AS_REP asRep = kdc.AS(asReq);
			
			#endregion
			
			#region TGS_REQ
			
			User bob = new User(userRemo);
			Authenticator auth = new Authenticator(aliceKey);
			KRB_TGS_REQ tgsReq = new KRB_TGS_REQ(asRep.GetTGT(aliceKey), auth, bob);
			KRB_TGS_REP tgsRep = kdc.TGS(tgsReq);
			
			#endregion
			
			#region AP_REQ
			
			Ticket ticket = tgsRep.GetTicket(aliceKey);
			string bobUrl = (string)ConfigurationSettings.AppSettings["RemotingUser"];
			Server bobServer = (Server)Activator.GetObject(typeof(Workstation.Server), bobUrl);
			KRB_AP_REQ apReq = new KRB_AP_REQ(ticket, auth);
			KRB_AP_REP apRep = bobServer.AP(apReq);
			
			#endregion
		}
	}
}