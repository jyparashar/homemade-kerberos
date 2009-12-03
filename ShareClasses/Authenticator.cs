
using System;

namespace ShareClasses
{	
	[Serializable]
	public class Authenticator
	{
		private byte[] encrypted;
		
		public Authenticator(Key key)
		{
			DateTime now = DateTime.Now;
			
			this.encrypted = DesEncryption.EncryptObject(now, key);
		}
		
		public DateTime GetTimestamp(Key key)
		{
			return (DateTime) DesEncryption.DecryptObject(this.encrypted, key);	
		}
	}
}
