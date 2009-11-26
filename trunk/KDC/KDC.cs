using System;
using System.Data;
using System.Configuration;
using ShareClasses;


namespace KDC
{	
	public class KDC : System.MarshalByRefObject, ShareClasses.IKDC
	{		
		private Key K_KDC;
		private int maxTimestamp;
		
		public KDC()
		{
			this.K_KDC = new Key("ABCDEFGH");
			this.maxTimestamp = (int)ConfigurationSettings.AppSettings["max_timestamp"];
		}

		private Key GetUserKey(User u)
		{
			MySQLQueries mysql = new MySQLQueries(
				ConfigurationSettings.AppSettings["db_host"], ConfigurationSettings.AppSettings["db_db"],
				ConfigurationSettings.AppSettings["db_user"], ConfigurationSettings.AppSettings["db_pass"]);
			mysql.OpenConnection();
			// TODO: Check MySQL Injection
			IDataReader reader = mysql.Select("SELECT key FROM " + ConfigurationSettings.AppSettings["db_pass"] + " WHERE user=\"" + u.Name + "\"");
			reader.Read();
			Key ka = new Key((string) reader["key"]);
			mysql.CloseConnection();
			
			return ka;
		}
		
		/// <summary>
		/// Obtaining a session Key and TGT
		/// </summary>
		/// <param name="u">
		/// A <see cref="User"/>
		/// </param>
		/// <param name="sk">
		/// A <see cref="SessionKey"/>
		/// </param>
		/// <param name="tgt">
		/// A <see cref="TGT"/>
		/// </param>
		public KRB_AS_REP AS(User u)
		{
			// Invent key SA
			Key sa = new Key(128);
			sa.CreateRandomKey();
			
			// Finds User's master key Ka
			Key ka = GetUserKey(u);	
			
			// TGT = K_KDC[User, SA]
			TGT tgt = new TGT(this.K_KDC, u, sa);
			
			// Return KA(SA, TGT)
			KRB_AS_REP krb_as_rep = new KRB_AS_REP(ka, sa, tgt);
			return krb_as_rep;
		}
		
		public KRB_TGS_REP TGS(TGT tgt, Authenticator auth, User userToTalk)
		{
			// Invent key K_AB
			Key k_ab = new Key(128);
			k_ab.CreateRandomKey();
			
			// Decrypt TGT to get SA
			Key sa = tgt.GetSA(this.K_KDC);
			
			// Decrypt authenticator
			DateTime timestamp = auth.GetTimestamp(sa);
			
			// Verifies timestamp
			DateTime now = DateTime.Now;
			int diffTimestamp = now.Second - timestamp.Second;			
			if (diffTimestamp > this.maxTimestamp)
			{
				// Fail in the timestamp
				return null;
			}
			
			// Finds Bob's master key KB
			Key k_b = GetUserKey(userToTalk);
			
			// Ticket to Bob = K_B[Alice, K_AB]
			Ticket ticket = new Ticket(k_b, tgt.GetUser(this.K_KDC), k_ab);
			
			
		}
	}
}
