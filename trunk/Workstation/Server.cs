using System;
using ShareClasses;

namespace Workstation
{	
	public class Server : MarshalByRefObject
	{		
		private Key workstationKey;
		
		public Server(Key workstationKey)
		{
			this.workstationKey = workstationKey;
		}
		
		public KRB_AP_REP AP(KRB_AP_REQ req)
		{
			// Decrypt ticket to get K_AB
			Key k_ab = req.Ticket.GetShareKeys(this.workstationKey);
		
			// Verifies timestamp
			DateTime timestamp = req.Authenticator.GetTimestamp(k_ab);
			
			KRB_AP_REP rep = new KRB_AP_REP(k_ab, timestamp);
			
			return rep;
		}
	}
}
