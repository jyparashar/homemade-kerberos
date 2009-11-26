using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

namespace ShareClasses
{
	public class DesEncryption
	{		
		public static byte[] EncryptObject(Object obj, Key key)
		{
			return ObjectToByteArray(obj);
			//return this.EncryptByteArray(this.ObjectToByteArray(obj), key);	
		}
		
		public static Object DecryptObject(byte[] bytes, Key key)
		{
			return ByteArrayToObject(bytes);
			//return this.ByteArrayToObject(this.DecryptByteArray(bytes, key));
		}
				
		// Convert an object to a byte array
		private static byte[] ObjectToByteArray(Object obj)
		{
		    if(obj == null)
		        return null;
		    BinaryFormatter bf = new BinaryFormatter();
		    MemoryStream ms = new MemoryStream();
		    bf.Serialize(ms, obj);
		    return ms.ToArray();
		}
		
		// Convert a byte array to an Object
		private static Object ByteArrayToObject(byte[] arrBytes)
		{
		    MemoryStream memStream = new MemoryStream();
		    BinaryFormatter binForm = new BinaryFormatter();
		    memStream.Write(arrBytes, 0, arrBytes.Length);
		    memStream.Seek(0, SeekOrigin.Begin);
		    Object obj = (Object) binForm.Deserialize(memStream);
		    return obj;
		}
		
		private static byte[] EncryptByteArray(byte[] bytes, Key key)
		{
			MemoryStream ms = new MemoryStream();
			
			DES des = new DESCryptoServiceProvider();
			des.Key = ASCIIEncoding.ASCII.GetBytes(key.KeyString);
            des.IV = ASCIIEncoding.ASCII.GetBytes(key.KeyString);				

			CryptoStream encStream = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);		
			encStream.Write(bytes, 0, bytes.Length);
			
			byte[] encU1 = ms.ToArray();
			encStream.Close();	
			
			return encU1;
		}
			
		private static byte[] DecryptByteArray(byte[] bytes, Key key)
		{
			// TODO
			return new byte[1];	
		}
	}
}