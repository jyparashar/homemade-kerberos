
using System;

namespace ShareClasses
{
	public interface IKdc
	{
		KRB_AS_REP AS(KRB_AS_REQ req);		
		KRB_TGS_REP TGS(KRB_TGS_REQ req);
	}
}
