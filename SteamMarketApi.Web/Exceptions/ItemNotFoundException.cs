using System;

namespace SteamMarketApi.Web.Exceptions
{
	public class ItemNotFoundException : Exception
	{
		public ItemNotFoundException(string message = "") : base(message)
		{
		}
	}
}