
using System;

namespace ShareClasses
{	
	public class KRB_AP_REQ
	{
		private int krbVersion = 4;
		private int reqKeyVersion = 1;
		private string reqRealm;
		private Ticket ticket;
		private Authenticator authenticator;
		
		public int KrbVersion
		{
			get { return this.krbVersion; }
		}
		
		public int ReqKeyVersion
		{
			get { return this.reqKeyVersion; }	
		}
		
		public string ReqRealm
		{
			get { return this.reqRealm; }	
		}
		
		public Ticket Ticket
		{
			get { return this.ticket; }	
		}
		
		public Authenticator Authenticator
		{
			get { return this.authenticator; }	
		}
		
		public KRB_AP_REQ(Ticket ticket, Authenticator authenticator)
		{
			this.ticket = ticket;
			this.authenticator = authenticator;
		}
	}
}
