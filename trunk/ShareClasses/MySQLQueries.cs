using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace ShareClasses
{
	public class MySQLQueries
	{		
		private IDbConnection dbcon;
		private IDbCommand dbcmd;
		private IDataReader reader;
		private string connectionString = "";
		
		public MySQLQueries(string server, string database, string username, string password)
		{
			this.connectionString =
				"Server=" + server +
				";Database=" + database +
				";User ID=" + username +
				";Password=" + password +
				";Pooling=false";
		}
		
		public void OpenConnection()
		{				
			this.dbcon = new MySqlConnection(this.connectionString);
			this.dbcon.Open();
			this.dbcmd = this.dbcon.CreateCommand();
		}
		
		public void CloseConnection()
		{
			this.reader.Close();
			this.reader = null;
			this.dbcmd.Dispose();
			this.dbcmd = null;
			this.dbcon.Close();
			this.dbcon = null;
		}
		
		public IDataReader Select(string selectQuery)
		{
			this.dbcmd.CommandText = selectQuery;
			this.reader = this.dbcmd.ExecuteReader();
			return this.reader;
		}
	}
}
