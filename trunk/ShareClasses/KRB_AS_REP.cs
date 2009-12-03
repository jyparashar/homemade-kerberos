using System;

namespace ShareClasses
{	
	[Serializable]
	public class KRB_AS_REP
	{
		[Serializable]
		private class KRB_AS_REP_NoEncrypted
		{
			public Key ks_a;
			public Tgt tgt;
		}
		private byte[] encrypted;
		
		public KRB_AS_REP(Key k_a, Key ks_a, Tgt tgt)
		{
			KRB_AS_REP_NoEncrypted noEncrypt = new KRB_AS_REP_NoEncrypted();
			noEncrypt.ks_a = ks_a;
			noEncrypt.tgt = tgt;
			
			this.encrypted = DesEncryption.EncryptObject(noEncrypt, k_a);
		}
		
		public Key GetKS_A(Key key)
		{
			KRB_AS_REP_NoEncrypted noEncrypt = (KRB_AS_REP_NoEncrypted) DesEncryption.DecryptObject(this.encrypted, key);	
			return noEncrypt.ks_a;
		}
		
		public Tgt GetTGT(Key key)
		{
			KRB_AS_REP_NoEncrypted noEncrypt = (KRB_AS_REP_NoEncrypted) DesEncryption.DecryptObject(this.encrypted, key);	
			return noEncrypt.tgt;
		}
	}
}
