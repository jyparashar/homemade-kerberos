using System;

namespace ShareClasses
{	
	public class KRB_AS_REQ
	{
		private int krbVersion = 4;
		private User user;
		private int desiredTicketLifetime = 5*60;
		private ServiceNames serviceName = ServiceNames.krbtgt;
		private string servicesInstance;
		
		#region Public properties
		
		public int KrbVersion
		{
			get { return this.krbVersion; }	
		}
		
		public User User
		{
			get { return this.user; }
		}
		
		public int DesiredTicketLifetime
		{
			get { return this.desiredTicketLifetime; }
		}
		
		public ServiceNames ServiceName
		{
			get { return this.serviceName; }	
		}
		
		public string ServicesInstance
		{
			get { return this.servicesInstance; }	
		}
		
		#endregion
		
		public KRB_AS_REQ(User user)
		{
			this.user = user;
		}
	}
}
