
using System;

namespace ShareClasses
{
	
	
	public class KRB_AP_REP
	{
		private byte[] encrypted;
		
		public KRB_AP_REP(Key key, DateTime timestamp)
		{
			// TODO: timestamp+1
			/// ...
			/// 
			
			this.encrypted = DesEncryption.EncryptObject(timestamp, key);
		}
		
		public DateTime GetTimeStamp(Key key)
		{
			DateTime timestamp = (DateTime) DesEncryption.DecryptObject(this.encrypted, key);
			return timestamp;
		}
	}
}
