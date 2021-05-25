namespace SteamMarketApi.Web.Entities
{
	/// <summary>
	/// Предмет steam
	/// </summary>
	public class SteamItem : BaseEntity
	{
		/// <summary>
		/// Название предмета
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Название предмета где вместо пробелов символы %20(пробел)
		/// </summary>
		public string NormalizedName { get; set; }

		/// <summary>
		/// Название в маркете
		/// </summary>
		public string MarketName { get; set; }

		/// <summary>
		/// Хешированное название
		/// </summary>
		public string MarketHashName { get; set; }

		/// <summary>
		/// Картинка шмотки
		/// </summary>
		public string Image { get; set; }

		/// <summary>
		/// Самый дешёвый предмет
		/// </summary>
		public decimal LowestPrice { get; set; }

		/// <summary>
		/// Средняя цена предмета
		/// </summary>
		public decimal MedianPrice { get; set; }

		/// <summary>
		/// Игра из которой предмет
		/// </summary>
		public GameType Game { get; set; }

		/// <summary>
		/// Айди приложения
		/// </summary>
		public int AppId { get; set; }
	}
}