using System;

namespace ShareClasses
{	
	[Serializable]
	public class KRB_TGS_REQ
	{
		private int krbVersion = 4;
		private int kdcKeyVersion = 1;
		private string kdcRealm;
		private Tgt tgt;
		private Authenticator authenticator;
		private int desiredTicketLifetime = 5*60;
		private User reqUser;
		
		#region Public properties
		
		public int KrbVersion
		{
			get { return this.krbVersion; }
		}
		
		public int KdcKeyVersion
		{
			get { return this.kdcKeyVersion; }	
		}
		
		public string KdcRealm
		{
			get { return this.kdcRealm; }	
		}
		
		public Tgt Tgt
		{
			get { return this.tgt; }	
		}
		
		public Authenticator Authenticator
		{
			get { return this.authenticator; }	
		}
		
		public int DesiredTicketLifetime
		{
			get { return this.desiredTicketLifetime; }	
		}
		
		public User ReqUser
		{
			get { return this.reqUser; }	
		}
		
		#endregion
		
		public KRB_TGS_REQ(Tgt tgt, Authenticator authenticator, User reqUser)
		{
			this.tgt = tgt;
			this.authenticator = authenticator;
			this.reqUser = reqUser;
		}
	}
}
