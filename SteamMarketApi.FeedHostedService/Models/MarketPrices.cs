namespace SteamMarketApi.FeedHostedService.Models
{
	//"name":"Auspicious Bracers of Bird's Stone","hash_name":"Auspicious Bracers of Bird's Stone","sell_listings":53,"sell_price":110,
	//"sell_price_text":"1,10 pуб.","asset_description":{"appid":570,"classid":"340910769","instanceid":"340885147","background_color":"","icon_url":"-9a81dlWLwJ2UUGcVs_nsVtzdOEdtWwKGZZLQHTxDZ7I56KW1Zwwo4NUX4oFJZEHLbXK9QlSPcU0vAlUQ0KfRfas1MrBQGJ7IztVv6ihODhzx_zGdHJA_t21kZKYqPjyDLnYhG9C19ZlhefEu9_2igLlrxVtZDrwdtLHcwM-YFDW-gS9xrq90JLv7c-anHM2vyVxsy3D30vg1SOmxJE","tradable":1,"name":"Auspicious Bracers of Bird's Stone","name_color":"32CD32","type":"Наручи, Uncommon","market_name":"Auspicious Bracers of Bird's Stone","market_hash_name":"Auspicious Bracers of Bird's Stone","commodity":0},"sale_price_text":"1,06 pуб."}

	public class MarketPrices
	{
		public MarketPrice[] results { get; set; }
	}
	public class MarketPrice
	{
		public string name { get; set; }
		public string hash_name { get; set; }
		// price rub/74-currency usd
		public string sell_price_text { get; set; }
		public asset_description asset_description { get; set; }
	}

	public class asset_description
	{
		// https://community.akamai.steamstatic.com/economy/image/ + icon_url
		public string icon_url { get; set; }
	}
}