using System;
using ShareClasses;

namespace KDC
{	
	public class KDC : System.MarshalByRefObject, ShareClasses.IKDC
	{		
		public KDC()
		{

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
		public void AS(User u, out SessionKey sk, out TGT tgt)
		{
			sk = new SessionKey();
			tgt = new TGT();
		}
	}
}
