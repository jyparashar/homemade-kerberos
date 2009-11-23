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
		private User u;
		private Key sa;
		
		public TGT(User u, Key sa)
		{
			this.u = u;
			this.sa = sa;
		}
		
		public string Encrypt(Key k)
		{
						DES des = new DESCryptoServiceProvider();
			XmlSerializer serializer = new XmlSerializer(typeof(User));
			TextWriter tw = new StringWriter();
			serializer.Serialize(tw, u);
			/*mySerializer.Serialize(
			
			// To write to a file, create a StreamWriter object.
			StreamWriter myWriter = new StreamWriter("myFileName.xml");
			mySerializer.Serialize(myWriter, myObject);
			myWriter.Close();*/
		}
	}
}
