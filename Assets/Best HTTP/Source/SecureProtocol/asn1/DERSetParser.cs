#if !BESTHTTP_DISABLE_ALTERNATE_SSL && (!UNITY_WEBGL || UNITY_EDITOR)
#pragma warning disable
using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
    // TODO[asn1] Should be renamed/replaced with DLSetParser
	public class DerSetParser
		: Asn1SetParser
	{
		private readonly Asn1StreamParser m_parser;

		internal DerSetParser(Asn1StreamParser parser)
		{
			this.m_parser = parser;
		}

		public IAsn1Convertible ReadObject()
		{
			return m_parser.ReadObject();
		}

		public Asn1Object ToAsn1Object()
		{
			return DLSet.FromVector(m_parser.ReadVector());
		}
	}
}
#pragma warning restore
#endif
