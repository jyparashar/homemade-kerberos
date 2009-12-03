using System;
using System.Data;
using System.Configuration;
using ShareClasses;

namespace Kdc
{	
	public class Kdc : System.MarshalByRefObject, ShareClasses.IKdc
	{		
		/// <summary>
		/// Main key for KDC
		/// </summary>
		private Key k_kdc;
		
		private int maxTimestamp;
		
		public Kdc()
		{
			this.k_kdc = new Key("ABCDEFGH");
			this.maxTimestamp = int.Parse(ConfigurationSettings.AppSettings["max_timestamp"]);
		}

		private Key GetUserKey(User u)
		{
			MySQLQueries mysql = new MySQLQueries(
				ConfigurationSettings.AppSettings["db_host"], ConfigurationSettings.AppSettings["db_db"],
				ConfigurationSettings.AppSettings["db_user"], ConfigurationSettings.AppSettings["db_pass"]);
			mysql.OpenConnection();
			string sqlq = "SELECT `key` FROM `" + ConfigurationSettings.AppSettings["db_tb_users"] + "` WHERE `user`=\"" + u.Name + "\";";
			IDataReader reader = mysql.Select(sqlq);
			reader.Read();
			Key k_a = new Key((string) reader["key"]);
			mysql.CloseConnection();
			
			return k_a;
			
			// Possible exceptions:
			// - User doesn't exists
			// - MySQL connexion problem
		}
		
		public KRB_AS_REP AS(KRB_AS_REQ req)
		{
			// Invent key SA
			Key sa = new Key(7);
			sa.CreateRandomKey();
			
			// Finds User's master key Ka
			Key ka = GetUserKey(req.User);	
			
			// TGT = K_KDC[User, SA]
			Tgt tgt = new Tgt(this.k_kdc, req.User, sa);
			
			// Return KA(SA, TGT)
			KRB_AS_REP krb_as_rep = new KRB_AS_REP(ka, sa, tgt);
			return krb_as_rep;
		}
		
		public KRB_TGS_REP TGS(KRB_TGS_REQ req)
		{
			// Invent key K_AB
			Key k_ab = new Key(7);
			k_ab.CreateRandomKey();
			
			// Decrypt TGT to get SA
			Key sa = req.Tgt.GetKS_A(this.k_kdc);
			
			// Decrypt authenticator
			DateTime timestamp = req.Authenticator.GetTimestamp(sa);
			
			// Verifies timestamp
			DateTime now = DateTime.Now;
			int diffTimestamp = now.Second - timestamp.Second;			
			if (diffTimestamp > this.maxTimestamp)
			{
				// Fail in the timestamp
				return null;
			}
			
			// Finds Bob's master key KB
			Key k_b = GetUserKey(req.ReqUser);
			
			// Ticket to Bob = K_B[Alice, K_AB]
			Ticket ticket = new Ticket(k_b, req.Tgt.GetUser(this.k_kdc), k_ab);
			
			return new KRB_TGS_REP(sa, req.ReqUser, k_ab, ticket);
		}
	}
}
