using Microsoft.EntityFrameworkCore;
using SteamMarketApi.Web.Entities;

namespace SteamMarketApi.Web
{
	/// <summary>
	/// Контекст бд
	/// </summary>
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions options) : base(options) 
		{
		}

		/// <summary>
		/// Предметы стим
		/// </summary>
		public DbSet<SteamItem> SteamItems { get; set; }
	}
}