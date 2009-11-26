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
	public class TGT
	{		
		private class TGTNoEncrypted
		{
			public User u;
			public Key sa;
		}
		
		private byte[] encrypted;
		
		public TGT(Key kdc_key, User u, Key sa)
		{
			TGTNoEncrypted tgtNoEncrypted = new TGTNoEncrypted();
			tgtNoEncrypted.u = u;
			tgtNoEncrypted.sa = sa;
			
			this.encrypted = new ObjectDESEncryption().EncryptObject(tgtNoEncrypted, kdc_key);
		}
		
		public User GetUser(Key key)
		{
			TGTNoEncrypted noEncrypt = (TGTNoEncrypted)new ObjectDESEncryption().DecryptObject(this.encrypted, key);	
			return noEncrypt.u;
		}
		
		public TGT GetSA(Key key)
		{
			TGTNoEncrypted noEncrypt = (TGTNoEncrypted)new ObjectDESEncryption().DecryptObject(this.encrypted, key);	
			return noEncrypt.sa;
		}
	}
}
