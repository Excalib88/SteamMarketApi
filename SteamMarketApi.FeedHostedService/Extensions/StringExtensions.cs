using System;

namespace SteamMarketApi.FeedHostedService.Extensions
{
	public static class StringExtensions
	{
		public static decimal ParsePrice(this string text)
		{
			if (string.IsNullOrWhiteSpace(text)) return 0;
			var priceNumber = text.Replace("$", "").Replace(" USD", "").Replace(",", "").Replace(".", ",");
			
			var isParsePrice = decimal.TryParse(priceNumber, out var priceDecimal);

			if (!isParsePrice) throw new Exception("Не удалось спарсить цену");

			return priceDecimal;
		}
	}
}