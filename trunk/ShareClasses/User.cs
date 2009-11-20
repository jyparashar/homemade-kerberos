
using System;

namespace ShareClasses
{
	
	[Serializable]
	public class User
	{		
		private string name;
		
		public String Name
		{
			get { return this.name; }
		}
		
		public User(string name)
		{
			this.name = name;
		}
	}
}
