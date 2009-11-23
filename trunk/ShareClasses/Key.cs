
using System;

namespace ShareClasses
{
	
	
	public class Key
	{
		private int size = 128;
		private string key = "";
		
		public string KeyString
		{
			get { return this.key; }	
		}
		
		public Key(int size)
		{
			if (size < 1)
				throw new Exception("Key must have a size bigger than 0.");
			this.size = size;
		}
		
		public Key(string key)
		{
			this.key = key;
			this.size = key.Length;
		}
		
		public Key()
		{
		}
		
		public void CreateRandomKey()
		{
			for (int i = 0; i < this.size; i++)
			{
				int randomInt = new Random().Next(1,255);
				this.key += Char.ConvertFromUtf32(randomInt);
				System.Threading.Thread.Sleep(randomInt);
			}
		}
	}
}
