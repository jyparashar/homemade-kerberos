
using System;

namespace ShareClasses
{	
	public class Ticket
	{
		private class Ticket_NoEncrypted
		{
			public User from;
			public Key shareKeys;
		}
		
		private byte[] encrypted;
		
		public Ticket(Key key, User from, Key shareKeys)
		{
			Ticket_NoEncrypted tne = new Ticket_NoEncrypted();
			tne.from = from;
			tne.shareKeys = shareKeys;
			
			this.encrypted = DesEncryption.EncryptObject(tne, key);
		}
		
		public User GetFrom(Key key)
		{
			Ticket_NoEncrypted ticket_noEncrypted = (Ticket_NoEncrypted) DesEncryption.DecryptObject(this.encrypted, key);
			return ticket_noEncrypted.from;
		}
		
		public Key ShareKeys(Key key)
		{
			Ticket_NoEncrypted ticket_noEncrypted = (Ticket_NoEncrypted) DesEncryption.DecryptObject(this.encrypted, key);
			return ticket_noEncrypted.shareKeys;
		}
	}
}
