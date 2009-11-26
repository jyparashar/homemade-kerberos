
using System;

namespace ShareClasses
{	
	public class Authenticator
	{
		private byte[] encrypted;
		
		public Authenticator(Key key)
		{
			DateTime now = DateTime.Now;
			
			this.encrypted = new ObjectDESEncryption().EncryptObject(now, key);
		}
		
		public DateTime GetTimestamp(Key key)
		{
			return (DateTime)new ObjectDESEncryption().DecryptObject(this.encrypted, key);	
		}
	}
}
