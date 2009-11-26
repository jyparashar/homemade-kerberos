using System;

namespace ShareClasses
{	
	public class KRB_AS_REP
	{
		private class KRB_AS_REP_NoEncrypted
		{
			public Key sa;
			public TGT tgt;
		}
		private byte[] encrypted;
		
		public KRB_AS_REP(Key ka, Key sa, TGT tgt)
		{
			KRB_AS_REP_NoEncrypted noEncrypt = new KRB_AS_REP_NoEncrypted();
			noEncrypt.sa = sa;
			noEncrypt.tgt = tgt;
			
			this.encrypted = new ObjectDESEncryption().EncryptObject(noEncrypt, ka);
		}
		
		public Key GetSA(Key key)
		{
			KRB_AS_REP_NoEncrypted noEncrypt = (KRB_AS_REP_NoEncrypted)new ObjectDESEncryption().DecryptObject(this.encrypted, key);	
			return noEncrypt.sa;
		}
		
		public TGT GetTGT(Key key)
		{
			KRB_AS_REP_NoEncrypted noEncrypt = (KRB_AS_REP_NoEncrypted)new ObjectDESEncryption().DecryptObject(this.encrypted, key);	
			return noEncrypt.tgt;
		}
	}
}
