
using System;

namespace ShareClasses
{	
	[Serializable]
	public class KRB_TGS_REP
	{
		[Serializable]
		private class KRB_TGS_REP_NoEncrypted
		{
			public User reqUser;
			public Key k_ab;
			public Ticket ticket;
		}		
		
		private byte[] encrypted;
		
		public KRB_TGS_REP(Key key, User reqUser, Key k_ab, Ticket ticket)
		{
			KRB_TGS_REP_NoEncrypted noEncrypt = new KRB_TGS_REP_NoEncrypted();
			noEncrypt.reqUser = reqUser;
			noEncrypt.k_ab = k_ab;
			noEncrypt.ticket = ticket;
			
			this.encrypted = DesEncryption.EncryptObject(noEncrypt, key);
		}
		
		public User GetReqUser(Key key)
		{
			KRB_TGS_REP_NoEncrypted noEncrypt = (KRB_TGS_REP_NoEncrypted) DesEncryption.DecryptObject(this.encrypted, key);
			return noEncrypt.reqUser;
		}
		
		public Key GetK_AB(Key key)
		{
			KRB_TGS_REP_NoEncrypted noEncrypt = (KRB_TGS_REP_NoEncrypted) DesEncryption.DecryptObject(this.encrypted, key);
			return noEncrypt.k_ab;
		}
		
		public Ticket GetTicket(Key key)
		{
			KRB_TGS_REP_NoEncrypted noEncrypt = (KRB_TGS_REP_NoEncrypted) DesEncryption.DecryptObject(this.encrypted, key);
			return noEncrypt.ticket;
		}
	}
}
