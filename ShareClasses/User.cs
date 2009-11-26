
using System;

namespace ShareClasses
{
	
	[Serializable]
	public class User
	{		
		private string name;
		private string instance;
		private string realm;
		
		public String Name
		{
			get { return this.name; }
		}
		
		public string Instance
		{
			get { return this.instance; }
		}
		
		public string Realm
		{
			get { return this.realm; }
		}
		
		public User(string name)
		{
			this.name = name;
		}
	}
}
