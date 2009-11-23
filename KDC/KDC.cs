using System;
using ShareClasses;

namespace KDC
{	
	public class KDC : System.MarshalByRefObject, ShareClasses.IKDC
	{		
		private Key K_KDC;
		
		public KDC()
		{
			this.K_KDC = new Key("123456789123456789qwertyuiopasdfghjklzxcvbnm");
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
			MySQLQueries mysql = new MySQLQueries(
				ConfigurationSettings.AppSettings["db_host"], ConfigurationSettings.AppSettings["db_db"],
				ConfigurationSettings.AppSettings["db_user"], ConfigurationSettings.AppSettings["db_pass"]);
			mysql.OpenConnection();
			// TODO: Check MySQL Injection
			IDataReader = mysql.Select("SELECT key FROM " + ConfigurationSettings.AppSettings["db_pass"] + " WHERE user=\"" + u.Name + "\"");
			reader.Read();
			Key ka = new Key((string) reader["key"]);
			mysql.CloseConnection();			
			
			// TGT = K_KDC[User, SA]
			TGT tgt = new TGT(this.K_KDC, u, sa);
			
			// Return KA(SA, TGT)
			return new KRB_AS_REP(ka, sa, tgt);
		}
	}
}
