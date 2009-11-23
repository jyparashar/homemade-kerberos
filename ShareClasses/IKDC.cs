
using System;

namespace ShareClasses
{
	public interface IKDC
	{
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
		KRB_AS_REP AS(User u);
	}
}
