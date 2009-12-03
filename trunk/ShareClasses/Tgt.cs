using System;
using System.IO;
using System.Xml.Serialization;
/// <summary>
/// http://msdn.microsoft.com/es-es/library/system.security.cryptography.des%28VS.80%29.aspx
/// </summary> 
using System.Security.Cryptography;

namespace ShareClasses
{	
	[Serializable]
	public class Tgt
	{		
		[Serializable]
		private class TgtNoEncrypted
		{
			public User u;
			public Key ks_a;
		}
		
		private byte[] encrypted;
		
		public Tgt(Key k_kdc, User u, Key ks_a)
		{
			TgtNoEncrypted tgtNoEncrypted = new TgtNoEncrypted();
			tgtNoEncrypted.u = u;
			tgtNoEncrypted.ks_a = ks_a;
			
			this.encrypted = DesEncryption.EncryptObject(tgtNoEncrypted, k_kdc);
		}
		
		public User GetUser(Key key)
		{
			TgtNoEncrypted noEncrypt = (TgtNoEncrypted) DesEncryption.DecryptObject(this.encrypted, key);	
			return noEncrypt.u;
		}
		
		public Key GetKS_A(Key key)
		{
			TgtNoEncrypted noEncrypt = (TgtNoEncrypted) DesEncryption.DecryptObject(this.encrypted, key);	
			return noEncrypt.ks_a;
		}
	}
}
